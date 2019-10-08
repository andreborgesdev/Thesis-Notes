# Entity Framework Core

Entity Framework is an ORM or Object-Relational Mapping

___Object-Relational Mapping___ - is a technique that lets you query and manipulate data from a database using a object-oriented paradigm. So in our code we can on objects rather than directly writing SQL statements in our code.

![Entity Framework](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Entity_Framework_Core.png?raw=true)

Entity Framework Core in an ORM but it is not an ugprade to Entity Framework 6. It is:

- A lightweight, extensible, and cross-platform version of Entity Framework
- Recommend for new applications that do not need the full EF6 feature set and/or .NET Core applications
- Supports a variety of databases, even non-relational ones
- Code-first or database-first approach

## Creating Entity Classes

We have to start by adding the entities to our application, these entities, like the DTO we've used before are simple classes, but the DTOs (outer-facing model), are different from the entities. Not all fields like computed fields are stored in a database and the data we want to offer to an API is often shaped differently than how it's stored in the underlying data store.

For the ID we should put a Key annotation and the way it generates the number, that are three possibilities for that:

- Null - no generation
- Identity - generation on add
- Computed - generation on add or update

The way thsis value is generated depends on the database provider being used, they may automatically set up value generation for some property types while other will require us to manually set up how the value is generated.

To create a relationship between two entities, by convention a relationship will be created when there is a navigation property discovered on a type. And a property is considered a navigation property if the type it points to cannot be mapped as a scalar type by the current database provider, so if we add a property City of type City, this is considered the navigation property, and a relationship will be created. Relationships that are discovered by convention will always target the primary key of the principle entity, most of the times IDs, that will be our foreign key. It is not required to explicitly define this foreign key property on the dependent class but it is recommended.

- Convention-based approach - states that a foreign key will be named according to the navigation property's class name followed Id.
- Or, for exaample, for navigation property City the foreign key on PointOfInterest is named CityId. We define that using the ForeignKey annotation

___Navigation Property:___

```csharp

[ForeignKey("CityId")]
public City City { get; set; }
public int CityId { get; set; }
```

It is best practice to ensure that the field restrictions are applied at the lowest possible level so in most of the cases it is the database itself. This ensures the best possible data integrity. So let's use annotation in our Entities.

## DbContext

It represents a session with the database and it can be used to query and save instances of our entities. Our entity classes are just classes, we did not need any extra dependencies to create those but the DbContext, that is part of Entity Framework Core and we will also need a provider.

Bigger aplications often use multiple contexts, for example, where we do add some sort of reporting module to our application, that would fit in a separate context. There is no need for all the entities that map to tables in a database to be in the same context. Multiple contexts can work on the same database.

DbSet is used to query and save instances of its entity type. LINQ queries against a DbSet will be translated into queries against the database.

We need to register the context so it is available for dependency injection. We do this by adding the dbcontext to the services - services.AddDbContext();

We need to provide a connection string to our DbContext. There are 2 ways of doing this:

- Through overriding the OnConfigure method on the DbContext, this has an OptionBuilder as parameter, and it provides us with a method UseSqlServer. This tells the DbContext it is being used to connect to a SQLServer database and it is here that we can provide a connection string.
- Via de constructor, what this allow us to do, that we can not when overriding, is that we can provide options at the moment we register ou DBContext, and that is a more logical approach, it is more in line with how we work with other services.

## Working with migrations

Just as our code evolves, so does the database. New tables might be added after a while, existing tables might be dropped or altered. Migrations allow us to provide code to change the database from one version to another. They are an important part of almost all applications.

We first need to create an snapshot of our database. In the Entity Framework Core world, it is achieved with tooling. These tools are essentially just another set of dependencies that add commands we can execute. We use the package Microsoft.EntityFramework.Tools. After we need to ensure we can actually use the commands contained in this dependency, for that we open project.json and we look for the tool section, this is not needed for .NET Core 2.X+.

Then we have to create an initial snapshot, or migration of our database and schema. For that we have to be able to execute one of the commands we just enabled and we can execute them in the package manager console. We can get it via TOOLS->NuGet Package Manager->Package Manager Console. The command we are looking for is the AddMigrationCommand. It expects a name for the migration we are going to add. This will generate a new Migrations folder that contains two files.

- A snapshot of our current context model - this contains the current model as we defined through our entities, including the annotations we provided for all entities.
- It is the file with the name we just provided to our migration - it cointains the code needed by the migration builder to build this version of the database, both Up (from current to new version) and Down (from this version to the previous version). If we look at Up we see two CreateTable statements and a CreateIndex statement. That means it is starting from no database at all and this migration contains the code to build the initial database. If we look at Down we see what should happen to end up with an empty database - two DropTable statements. If we migrations are added, new files like this will be created and by executing them in order our database can evolve together with our code. We do not need to generate the Add-Migration command to generate these files, we could have written them by hand, but it is not something we want to do to a big database.

Next we have to ensure that the migration is effectively applied to our database and we will use a command for that, it's called "Update-Database" if we execute this, the migrations will be applied to our current database but we can also do this from code if on the constructor of the context we run Database.Migrate() instead of Database.EnsureCreated(). This will execute migrations, which, if there is no database, will create it. But if there is one it will throw an error because it will try to create a table that already exists so we have to delete the database and migrate it with the existing files from the snapshot. It is a better approach to start from the beggining with Database.Migrate(). After this, everytime we do Add-Migration and then we run the code with the Migrate on the constructor it will update the database with our code changes, for example, the addition of a parameter on an entity.

## Safely Storing Sensitive Configuration Data

We might want to store, for example, our database connection string on the appSettings. As long as we're working on our local machine in a development environment on our LocalDB instance we'll be ok. But once we are deploying to production we'll be providing a connection string to an actual SQL Server Instance. We're currently working with integrated security with the "trusted connection" part of the connection string but sometimes a connection string contains a username/password combination. We do not want to expose nor that info nor the name of our actual produtcion database.

First, get rid of the hard-coded connection string by adding it to the appSettings file and since we have files for the 2 environments we can change it. During development we use the appSettings file and then we get the value via the configuration object in the Startup file. On the production environment we want to use envorionment variables. To use environment variables we have to add the method AddEnvironmentVariables() to the builder on the Startup constructor.

![Environment Variables](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Environment_Variables_Connection_String.png?raw=true)

After we create our environment variables for the production environment. So we have told our application to look into the environment variables for configuration information so what will happen is that if the environment variable with the same key is found in a certain environment, production in our case, it will override the value from other configuration sources as AddEnvironmentVariables was the last configuration source added to the build chain.

Now we have 3 providers for the Connection String (2 appSettings for development and production and 1 Environment variable). With the Environment variable the connection string is not submitted to the source control.

This works great for Visual Studio, however, those setting we set in the project properties, the environment variables, are actually added to a launchSettings.json file and this not something we deploy to a production server nor check into source control, by the way. So this is still not a good option so we should delete our connection string from there the VS launchSettings or Project Properties, it is the same.

The way of storing our connection string that we want to use is on the Environment Variables of the system itself (System properties). We do not have to change any code for the code that we use for the VS Environment Variables to work on the system ones.

![Environment Variables System](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Environment_Variables_Connection_String_System.png?raw=true)

As environment variables, in our case, override all the other configuration sources this connection string will be used regardless of the environment we choose in Visual Studio.

The general rule is

- use appSettings for non-sensitive data
- use the environment variables we set in our project properties only on our local machine and never submit them to source control, to the launchSettings file
- once we roll out to the production server, set the environment variables as evironment variables from the system.

There is still a risk because environment variables are typically unencrypted key/value pairs so if someone gains access to the machine or process he can read out that data. But this approach is already safer then using a text file.

## Seeding the Database with Data

Is the principle of providing the database with data to start with. It is often used to provide master data.

AddRange makes the entities tracked by the context but they are not inserted yet, or that we must use SaveChanges because that will effectively execute the statements on our database. On the configure method of the Startup file we must create a context for our entity and make a EnsureSeedDataForContext.