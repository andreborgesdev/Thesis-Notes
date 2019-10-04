# API

ASP.NET Core has the Web API (HTTP Services) and MVC (client-facing web applications) together, there is no need to create two different projects as we needed to in the past. ASP.NET Core MVC.

## MVC

- Model - handles the logic for the application data. A model in this sense can contain code to retrieve or store data at that level. In some implementations, often when the MVC pattern is only used at the top level of the architecture, the model doesn't contain any logic at all. It's another component of the application that handles this.
- View - Represents the parts of the application that handle the display of data. This might be HTML, for example.
- Control - Handles the interaction between the view and the model, including handling user input.
- It is an architectural software pattern for implementing user interfaces
- Loose coupling, separation of concerns: testability, reuse
- It is not the full application architecture. In a typical n-tier architecture (Presentation layer, Business layer and Data access layer), more often than not with a service layer in between, MVC is used in the presentation layer. Hence, the implementing user interface part of the definition.

![MVC Model](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/MVC.png?raw=true)

## ASP.NET Core 2

Modularity comes with a set of potential issues

ASP.NET Core 1 applications:

- It can be hard to find the functionality we need
- Keeping track of version numbers is cumbersome

ASP.NET Core 2 Introduced two new conecpts:

- Metapackage - Microsoft.AspNetCore.All metapackage is referenced by default for new ASP.NET Core 2 applications and it includes all supported ASP.NET Core packages, all supported Entity Framework Core Packages and Internal and 3rd-party dependencies used by ASP.NET Core and Entity Framework Core. It adds reference to a list of packages.
- Runtime Store - Is a location on disk containing these (and other) packages which gives us a Faster deployment, lower disk space use and improved startup performance. But it comes with a trade-off, important to know is that when using this approach and publishing the app, the required assemblies are no longer copied to your application folder, whereas on the ASP.NET Core 1 they were. That means that to use this approach, we will have to install the runtime and thus the runtime store on each machine we want to publish to ASP.NET.

 There is two types of Runtime Store:

- Self-contained - It's the default for ASP.NET Core 1.x and we do not need to install the runtime on the machine we're publishing the application into.
- Framework dependent - It's the default for ASP.NET Core 2.0 and the correct version of the runtime must be installed on the machine we're publishing to. Fortunately this is optional and we can still add the packages we need instead of metapackages.

## Routing

Matches the request URI to a controller method. It can do it in two way, Convention-based and attribute-based routing

- Convention-based Routing - Conventions need to be configures. It is not advised for API's, it is better for HTML-returning views.

- Attribute-based Routing - Attributes at contoller & action level including an (optional) template. The URI is matched to a specific action on a controller

![Routing](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Routing.png?raw=true)

## Status Codes

Part of the response.

Provide information on

- Whether or not the request worked out as expected
- What is responsible for a failed request

The 5 levels of Status Codes

- Level 100 Status Codes are informational and were not part of the HTTP 1.0 standard. These are currently not used by API's
- Level 200 (SUCCESS) mean the request went well. The most common ones are 200 for a successful GET request, 201 a successful requet that resulted in the creation of a new resource and 204 for no content
- Level 300 are used for redirection. Most API's do not have need for this.
- Level 400 (CLIENT ERROR) tells the consumer he did something wrong. 400 means Bad Request, the request that the consumer of the API sent to the server is wrong. 401 Unauthorized which means that no or invalid authentication details were provided. 403 Forbidden, means that the authentication succeeded but the authenticated user does not have access to the requested resource. 404 Not Found means that the request resource does not exist. 409 is used for conflicts
- Level 500 (SERVER ERROR). Often only the 500 Internal Server Error is supported. It means that the server made a mistake, and the client can not do anything about it other than try again later.

app.UseStatusCodePages() if we want the status code to appear on the browser page instead of only on the developer tools

## Serializer Settings

In the old API's .NET did not change the first letter of a parametner, .NET Core does and it puts it on lowercase so if there is a migration from .NET to .NET Core the consumer is not expecting the endpoints of the API to change values, so we have to change it. Now is camel case.

.NET CORE by default deserialized from and serialized to JSON. To configure how that serialization happens we have to configure our service.

```csharp
services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(o =>
                {
                    if (o.SerializerSettings.ContractResolver != null)
                    {
                        var castedResolver = o.SerializerSettings.ContractResolver
                            as DefaultContractResolver;
                        castedResolver.NamingStrategy = null;
                    }
                });
```

That way it will simply take the property names as they are defined on our class.

Example: id -> Id

We might need this or not. For most new applications the default lowercase letter to start with, will be what's required.

## Formatters and Content Negotiation

Selecting the best representation for a given response when there are multiple representation available.

Media type is passed via the Accept header of the request

- application/json
- application/xml
- ...

We can set a default format, in this day, JSON.

ASP.NET Core supports this via output formatters. An output format deals with outputs. The consumer of the API can request a specific type of output by settings the Accept header to the requested media type. But if there is output there is also input. The input formatter deals with input, for example, the body of a POST request for creating a new resource, or input do media type of the request body is identified through the content-type header.

- Output formatters - Accept header
- Input formatters - Content-type header

```csharp
services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));
```

## Manipulating Resources

Creating, updating and deleting resources and Validating Input.

## Creating a Resource

There are systems where the consumer is responsible for choosing the ID, so it is a valid use case, and in those cases it will lead to the PointOfInterestDto and PointOfInterestForCreationDto to contain the exact same fields. But even in those cases it is still better to keep these separate, it leads to a model that's more in line with API functionality making change or refactoring afterwards easier and when validation comes into play we might want validation on input but that is not necessarily the same validation, if at all, that is needed for output. So it is better to create a separate DTO for creating, for updating and for returning resources.

