# ASP.NET Core

Framework for building modern cloud-based internet connected applications

Open-source

Cross-platform

Rethought from the ground up =/= from ASP.NET 4

Granular set of NuGet packages

Smaller application surface area:

- Tighter security
- Reduced servicing
- Improved performance

The applications can run on the Full .NET Framework or .NET Core

## .NET CORE

- Modular version of the .NET Framework
- Portable across platforms
- Subset of .NET Framework
- Web: Windows, Linux, Mac
- Other: desktop, devices, phone
- Implementation of .NET Standard (defines a common base that a platform should support)

![Differences between ASP.NET CORE 1 and 2](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Differences_ASP.NET_CORE_1_&_2.png?raw=true)

![Differences between ASP.NET CORE 1 and 2 - Releases](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Differences_ASP.NET_CORE_1_&_2_Releases.png?raw=true)

# Programa

## Program.cs

It's the starting point of our program application.

ASP.NET Core is a console application that calls into ASP.NET specific libraries.

### Main

- responsible for configuring and running the application

### WebHostBuilder

- method that hosts the web server for the web application. ASP.NET Core is completely decoupled from the web server environment that hosts the application. It actually ships with two different HTTP servers:

- WebListener - is a Windows only server
- Kestrel - cross-platform server. Is the default.

As we are in Visual Studio it uses the IIS Express as it default web host. It functions as a reverse proxy server for Kestrel. If running on a Windows Server we should run IIS as a reverse proxy server that manages and proxies requests to Kestrel. If deploying on Linux we should run a comparable reverse proxy server, such as Apache, to proxy requests to Kestrel.

It is possible to self host our applicaion using Kestrel alone, but using something like IIS as a reverse proxy brings a lot of advantages, for example, IIS can filter requests, manage the DSL layer and certificates, make sure the application restarts if it crashes, etc.

### UseContentRoot

- specifies the content root directory used by the web host. It's the base path to any content used by the application, such as it's views and it's web content. By default it is the same as the application base path for the executable hosting the application, but we can give an alternative location if we want. It is not the same as the web root, the default web root is the "wwwroot" which is a location in a project from which HTTP requests are handled.

### UseStartup

- is used by the web host that it that other class that was created in the empty web application.

### Build

- builds an IWebHost instance to host our web application.

### Run

- runs the web application and it blocks the calling thread until the host shuts down.

### ___.NET CORE 2+___

It actually changes some things and with less code visible on this file it does more in the background, for example, it implements default files and variables for configuration and there is also logging.

## Startup.cs

Entry point for an application

### ConfigureServices

- add services to the container and configure those services. That container is used for dependency injection. It's an optional method, but most apps require it.

Service in .NET Core world is a component that is intended for common consumption in an application. There is Framework Services like Identity, MVC, Entity Framework Core services and there is also Application Services wich are application specific, for example, a component to send mail, that's an application specific service which will make available for dependency injection by registering it in this ConfigureServices method.

There are some services that are built-in like the Application Builder and a logger, those services are available for injection.

The services that we add in the ConfigureServices can later be injected into other pieces of code that live in our application. One example is the injection on the Configure method. An instance of an IApplicationBuilder interface implementing class, one that implements IHostingEnvironment and one that implements ILoggerFactory are provided by the container.

### Configure

- is called after the ConfigureServices method because it uses services that are registered and configured in that method. It's used to specify how a ASP.NET will respond the individual HTTP requests. This is where we'll have to let our application know it has to use MVC for handling HTTP requests.

## Pipeline and Middleware

The pieces of code the handle requests and result in responses make up the request ___pipeline___.

What we can do is configure that request pipeline by adding middleware, which are software components that are assembled into an application pipeline to handle requests and responses.

In .NET Core middleware took over the role of both modules and handlers.

Examples of middleware than can be added to this pipeline are diagnostic middleware or middleware handle authentication, and even MVC itself is a piece of middleware that can be added to the request pipeline.

The ASP.NET request pipeline consists of a sequence of request delegates called one after the next from one piece of middleware to the next. Each of those has the opportunity to perform operations before and after the next delegate. That eventually results in a response we need. Important to know is that each component chooses wether to pass the request on to the next component in the pipeline or not. So the order in which we add middleware to the request pipeline matters. A good example would be authentication middleware, if the middleware component finds that the request is not authorized, it will not pass it on to the next piece of middleware, but it will immediately return an Unauthorized response. It's, thus, important that the authentication middleware, in case of our example, is added before other components that should not be triggered in case of an unauthorized request.

### UseDeveloperExceptionPage

- This configures the request pipeline by adding the developer exception page middleware to the request pipeline. So now when an exception is thrown, this piece of middelware will handle it. The middleware is part of the Microsoft.ASPNETCore.Diagnostics assembly. Most middleware can be found in separate assemblies, a testament to the modularity of the ASP.NET Core. It will only be added when we are running in the development environment.

Environment =/= Type of build

With the native service IHostingEnvironment we can make operations on the everionment we are currently in or in different ones.

## Microsoft.AspNetCore.All

Microsoft.AspNetCore.All. That's what's called a metapackage reference. By adding this reference, we're actually adding references to all the dependencies you can see when we scroll down a bit. This is an ASP. NET Core 2-only feature. Now why would we do this? Well, if you've worked with ASP. NET Core before, you know that instead of using one big monolith assembly like System. web in the old ASP. NET, ASP. NET Core instead shows a more modular, granular approach. It's made up of a large set of small packages containing specific pieces of functionality, but that comes with its own set of issues. It's not always easy to find out in which package to the functionality you need can be found. Moreover, keeping an eye on using the correct versions of these assemblies can become quite cumbersome, but that's the way it is for ASP. NET Core 1. x applications. So for ASP. NET Core 2, this was solved with the Microsoft. AspNetCore. All metapackage. That metapackage includes all supported packages by the ASP. NET Core Team, all supported packages by the Entity Framework Core Team, and internal and third-party dependencies used by ASP. NET Core and Entity Framework Core. So this metapackage makes life a bit easier. However, it comes with a potential drawback. It requires you to install the. NET Core SDK on the server you're deploying to. That avoids having to deploy them together with the application. That's it. This is completely optional. If you don't like the metapackage reference, you can also manually add references to each specific package you want to use just as in ASP. NET Core 1.
