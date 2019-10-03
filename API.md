# API

ASP.NET Core has the Web API (HTTP Services) and MVC (client-facing web applications) together, there is no need to create two different projects as we needed to in the past. ASP.NET Core MVC.

## MVC

- Model - handles the logic for the application data. A model in this sense can contain code to retrieve or store data at that level. In some implementations, often when the MVC pattern is only used at the top level of the architecture, the model doesn't contain any logic at all. It's another component of the application that handles this.
- View - Represents the parts of the application that handle the display of data. This might be HTML, for example.
- Control - Handles the interaction between the view and the model, including handling user input.
- It is an architectural software pattern for implementing user interfaces
- Loose coupling, separation of concerns: testability, reuse
- It is not the full application architecture. In a typical n-tier architecture (Presentation layer, Business layer and Data access layer), more often than not with a service layer in between, MVC is used in the presentation layer. Hence, the implementing user interface part of the definition.

## ASP.NET Core 2

Modularity comes with a set of potential issues

ASP.NET Core 1 applications:

- It can be hard to find the functionality we need
- Keeping track of version numbers is cumbersome

ASP.NET Core 2 Introduced two new conecpts:

- Metapackage - Microsoft.AspNetCore.All metapackage is referenced by default for new ASP.NET Core 2 applications and it includes all supported ASP.NET Core packages, all supported Entity Framework Core Packages and Internal and 3rd-party dependencies used by ASP.NET Core and Entity Framework Core. It adds reference to a list of packages.
- Runtime Store - Is a location on disk containing these (and other) packages which gives us a Faster deployment, lower disk space use and improved startup performance. But it comes with a trade-off, important to know is that when using this approach and publishing the app, the required assemblies are no longer copied to your application folder, whereas on the ASP.NET Core 1 they were. That means that to use this approach, we will have to install the runtime and thus the runtime store on each machine we want to publish to ASP.NET
