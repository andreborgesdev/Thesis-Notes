# API

ASP.NET Core has the Web API (HTTP Services) and MVC (client-facing web applications) together, there is no need to create two different projects as we needed to in the past. ASP.NET Core MVC.

## MVC

- Model - handles the logic for the application data. A model in this sense can contain code to retrieve or store data at that level. In some implementations, often when the MVC pattern is only used at the top level of the architecture, the model doesn't contain any logic at all. It's another component of the application that handles this.
- View - Represents the parts of the application that handle the display of data. This might be HTML, for example.
- Control - Handles the interaction between the view and the model, including handling user input.
- It is an architectural software pattern for implementing user interfaces
- Loose coupling, separation of concerns: testability, reuse
- It is not the full application architecture. In a typical n-tier architecture (Presentation layer, Business layer and Data access layer), more often than not with a service layer in between, MVC is used in the presentation layer. Hence, the implementing user interface part of the definition. An API can also be regarded as a user interface; it's the interface to the consumer of the API.

![MVC Model](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/MVC.png?raw=true)

The model handles the logic for our application data. A model in this sense can contain code to retrieve or store data at that level. In some implementations, view models are used as a model. These contain code to retrieve or store data, but in quite a few implementations the model doesn't contain any logic at all, it's another component of the application that handles this. For example, the business layer. The model, that's then the DTOs used by an API, those that are serialized into the response bodies, AKA serialized to resource representations. The view represents the parts of the application that handle displaying of data. So I'm building an API, this view should be regarded as the representation of our data or resources. More often than not these days, that is JSON. And the controller then handles the interaction between the view and the model, including handling user input. In the API world, this user that interacts with the API is the consumer, typically another application. If you look at the dependencies between these components, both controller and view depend on the model, and the controller also depends on the view. That's one of the key benefits of this separation. In other words, the controller chooses the view to display to the user and provides it with any model data it requires. So, when a request is made to an API by a consumer, an action on the controller will be triggered. The controller sends any data that's input, like query string parameters, to the part of the application responsible for business or data access logic. It then returns a model to the view, in this case the resource representation in JSON, for example.

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

## Validation

For validation we use some keywords on our model DTO, like Required, maxlength.

```csharp
[Required(ErrorMessage = "You should provide a name value")]
[MaxLength(50)]
public string Name { get; set; }

[MaxLength(200)]
public string Description { get; set; }
```

If there is no native validation for a rule we want to apply fror a parameter we can create it ourself. For example if the Name and Description of a Point of Interest is the same.
NOTE: this rule is applied on the controller and not on the DTO.

```csharp
if (pointsOfInterest.Description == pointsOfInterest.Name)
{
    ModelState.AddModelError("Description", "The provided description should be different from the name.");
}
```

The problem with this approach is that annotations mix in rules with models and that is not really good separtion of concerns and having to write validation rules in two different places, the model and the controller, for the same model does not feel right either, but this is the default approach used by .NET and .NET Core. However if we are building more complex applications it might be a good idea to check out something like FluentValidation, which offers a fluent interface to build validation rules for our objects.

## Updating a Resource

As seen previously it is a good practice to use different models for create, update and read, even if they contain the same properties.

There is two ways of updating a resource

- Full update - ___PUT___ - According to the HTTP standard, PUT should fully update the resource. That means that the consumer of the API must provide values for all fields for all fields of the resource, save for the ID, as that one is already coming for the URI. If a consumer does not provide a value for a field, that field should be put to its default value, which conveniently, it will have an inputted object. We must, thus, update all fields. Lastly, we return a tool for NoContent, that means that the request completed successfuly, but there is nothing to return. We can also return a 200 Okay containing the updated resource, but tipically for updates, we would return NoContent as the consumer already has all the information, after all, it is the consumer who send the content that had to be changed. The fields that are not provided by the user will have the default value of null. This is completely correct according to the HTTP standard, but it might not be ideal for consumers of the API.
- Partial update - ___PATCH___ - The body of a PATCH request contains only the fields that we want to update and, if we go further it also needs to contain an opertaion that has to happen, for example, a PATCH request that copies the value of one property to another property. There is a standard format for this. The standard is named JSON Patch (RFC 6902) and it describes a document structure for expressing a sequence of operations to apply to a JSON document. A JSON Patch document is essentialy a list of operations, like, at, remove, replace, etc. that have to be applied to the resource allowing for partial updates.

![PATCH Request](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/PATCH_Request.png?raw=true)

To validate the JsonPatchDocument we need to use the method TryValidateModel in our now patched instance since ModelState can not check it. This triggers validation of this model and any errors will also end up in the model state. For example to check removes on a parameter that can not be null.
