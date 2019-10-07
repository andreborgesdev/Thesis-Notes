# Dependency Injection and Services

## IoC - Inversion of Control and Dependency Injection

Example - Imange a class that uses different services, for example, when we implement logging in PointsOfInterestController it will use a logger service. That's a concrete type specified at design time, this means the PointsOfInterestController has a dependency on the logger. We tipically new it up in the constructor. Let's say it also depends on one or more other services. Problems arise when you need to replace or update that logger, the controller source code will have to change.

- Class implementation has to change when dependency changes
- Difficult to test
- Class manages the lifetime of the dependency
- This is tight coupling

___Inversion of Control___ - delegates the function of selecting a concrete implementation type for a class's dependencies to an external component.

___Dependency Injection___ - is a specialization of the Inversion of Control pattern. The Dependency Injection pattern uses an object - the container - to initialize objects and provide the required dependencies to the object. It makes it loose coupling, we have less code changes and better testability.

![Dependency Injection](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Dependency_Injection.png?raw=true)

Dependency Injection is built into ASP.NET Core

ConfiguraServices is used to register services with the built-in container

## Injecting and Using a Logger

The Logger is a built-in service, so we don't have to add it to the container in the ConfigureServices method, what we need to need is configure one or more loggers so the service knows what to log and where it should log to. We do that in the Configure method of the Startup class.

In .NET Core 2.X+ we add the loggers into our Program.cs class and not into our Startup class.

Constructor Injection is the preferred way of requesting dependencies, but for those cases where that's not feasible, we can always request one from the container directly.

Logging informational messages can help you find the cause of an error, but what we really want to log always, are exceptions (try catch).

## Logging to a File

Logging into the console during the development is normal but after we want to log into a more persistent place, like a file or a DB.

.NET Core does not have a built-in logger to a DB or a file. It does contain one for logging to the event log, but if we want to develop for cross-platform, that one is not available as the event log is a Windows concept. The built-in logging system was built in a way that allows third-party providers to easily integrate with it.

We use an third-party provider such as NLog (NLog.Exntensions.Logging) and configure it with a file called "nlog.config" on the root folder. Ax example of that config file can be found here - <https://github.com/NLog/NLog/wiki/NLog-config-Example>.

The configuration method changes across providers. What is the same across them is to tell the loggerFactory to use them, in the example of NLog we can use loggingFactory.AddNLog(). The nice part about the fact that these third-party providers integrate with the built-in logging system is that we don't have to change any other parts of our code, we already have to code to log in the PointsOfInterestController. That same code will keep on working now also using the extra provider to log to.
NOTE: The file generated might not always be generated in the root folder, it can go to bin->Debug.

## Implementing and Using a Custom Service

After we create our service we have to register it with the container. So we can inject it using the built-in dependency injection system on to the ConfigureServices method on the startup class.

When we look at the methods we have on services, our container, there is three that are of importance and allows us to register custom services, these refer to the lifetime the services:

- AddTransient - Transient lifetime services are created each time they are requested. This lifetime works best for lightweight stateless services
- AddScoped - Scoped lifetime services are created once per request
- AddSingleton - Singletone lifetime services are created the first time they are requested or if you specify an instance when ConfigureServices is run. Every subsequent request will use the same instance.
  
Using constructor injection we will inject our service in our controller. Now we know how to create a custom service, register it and inject it, but this does not really drive one of the points of using dependency injection home, and that points was that it delegates the function of selecting a concrete implementation type for the controller's dependencies.

A way to distinguish between 2 services using an interface is by using compiler directives, they tell a compiler to omit or include certain pieces of code on compile depending on the symbol used.

## Configuration Files

Almost all applications require some way of keeping and reading configuration values. ASP.NET Core can work with a variety of application configuration data sources, from JSON, XML and ini files over in-memory settings to command line arguments of environment variables. The most common one is the JSON file to store settings.

To work with settings in a ASP.NET Core app we should instantiate a Configuration in our application startup class. That configuration, which adheres to the IConfigurationRoot interface, should then be stored as a static property so we will have one instance application-wide. The best play to do that is in the Startup constructor if we do not have one we have to create one and we need to inject the host environment as we will need that to point the framework to the root of our app.

___ASP.NET Core 1___

```csharp
public class Startup
{
    public static IConfigurationRoot Configuration;

    public Startup(IHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("mailSettings.json", optional:false, reloadOnChange:true);

        Configuration = builder.Build();
    }
}
```

___ASP.NET Core 2___

```csharp
public class Startup
{
    public static IConfiguration Configuration { get; private set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
}
```

In .NET Core 2 it is different and has less code as the call into CreateDefaultBuilder on the Webhost in the program class already takes care of this for us.

This is really showing the modularity of ASP.NET Core. Thera are a lot of different assemblies and packages, some containing only small pieces of functionality, and that's a good thing, it avoids having big monolithic assemblies full of functionality we do not need and that is why it is good to start from an empty web project. All the other templates will already include quite a few dependencies, some of which we probably do not need.

Now that the configuration values from our mailSettigs file are accessible we have to access the configuration variable from startup from our service. We pass in the key and that will return the value from mailSettings.

## Scoping Configuration to Environments

We can scope configuration files to a specific environment by providing different configuration sources, for which part of the name is the name of the environment.

When we give it the same name "*.Prodiction", for example, it will recognise they are the same configuration file for different environments and it will add it as a child of one another.

So, by combining working with different environments, environment-scoped configuration files and compiler directives for registration at container level, we get a really granular approach to all of this and almost all the possible options we might need.
