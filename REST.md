# Rest

Most of the time when we hear this term, the first thing that comes to mind is APIs, it's how we build APIs, and JSON, because that's what we get back from these APIs, and that's a pretty common thing to say as all we read and hear about are, well, RESTful APIs that return JSON. But, REST isn't just about building an API which consists of a few HTTP services that return JSON. It's much broader than that.

Rest stand for Representational state transfer and is intended to evoke an image of how a well-designed web application behaves, a network of web pages where the user progresses through an application by selecting links (state transitions), resulting in the next page (representing the next state of the application) being transferred to the user and rendered for their use.

- REST is an architectural style, not a standard - we use standards to implement it
- REST is protocol agnostic - JSON isn't a part of REST, but theoretically not even HTTP is a part of REST, and that's really theoretical.

Imagine you want to read your favorite newspaper online. You've opened your browser. That browser, that's an HTTP client. You point it to a URI. That's the unique resource identifier. It identifies where the resource lives. By doing that, the browser actually sends an HTTP request to that URI. The server then does some magic and sends an HTTP response message back to the browser. That HTTP response message contains a representation of the page you've navigated to.  In our example, that would probably be some HTML and CSS. The browser then interprets that resource representation and shows it. In other words, the browser, our HTTP client, has changed state. Now let's say we click a link in our browser to access a specific article on the newspaper side. That one is again identified by a URI. A new request message is sent to the server, and the server again sends back a representation of the page, the resource. The browser interprets it and changes state. In other words, the client changes state depending on the representation of the resource we're accessing. And that's representational state transfer, or REST.

## REST Constraints

REST is defined by 6 constraints (one optional).

A contraint is a design decision that can have positive and negative impacts - for this, contraints that benefits are considered to outweight the disadvantages.

- Client-server constraint - a very basic fundamental constraint. What this does is enforce client-server architecture. A client, or consumer of the API in our lingo, shouldn't be concerned with how data is stored, or how the representation is generated, that's transparent. A server, the API in our lingo, shouldn't be concerned with, for example, the user interface or user state or anything related to how the client is implemented. In other words, client and server can evolve separately.
- Statelessness constraint - this means that the necessary state to handle every request is contained within the request itself. When a client requests a resource, that request contains all the information necessary to service the request.  It's one of the constraints that ensures RESTful APIs can scale so easily. We don't have things like server-side session state to keep in mind when scaling up.
- Cacheable constraint - This one states that each response message must explicitly state if it can be cached or not. Like this we can eliminate some client/ server interaction, and at the same time prevent clients from using out-of-date data.
- Layered system constraint - That's a pretty easy one actually. A REST-based solution can be comprised of multiple architectural layers, just as almost all application architectures we use today. These layers can be modified, added, removed, but no one layer can directly access a layer that's beyond the next one. That also means that a client cannot tell whether it's directly connected to the final layer or to another intermediary along the way. REST restricts knowledge to a single layer, which reduces the overall system complexity.
- Optional code on demand constraint - This one states that the server can extend or customize client functionality. For example, if your client is a web application, the server can transfer JavaScript code to the client to extend its functionality.
- Uniform interface constraint - as it's divided into four sub-constraints. It states that the API and the consumers of the API share one single technical interface. As we're typically working with the HTTP protocol, this interface should be seen as a combination of resource URIs, where we can find resources, HTTP methods, how we can interact with them, like GET and POST, and HTTP media types, like application/json, application/xml, or more specific versions of that that are reflected in the request and response bodies. All of these are standards, by the way. This makes it is a good fit for cross-platform development. By describing such a contract, this uniform interface constraint decouples the architecture, which in turn enables each part to evolve independently. The four subconstraint are:
  
    1. Identification of resources - It states that individual resources are identified in requests using URIs, and those resources are conceptually separate from the representations that are returned to the client. The server doesn't send an entity from its database or possibly a combination of fields from multiple additional databases and services because our author resource doesn't necessarily map to an author in one database. Instead it sends the data, typically for RESTful APIs, as JSON, but HTML, XML, or custom formats are also possible.
    2. Manipulation of resources through representations - When a client holds a representation of a resource, including any possible metadata, it has enough information to modify or delete a resource on the server, provided it has permission to do so. If the API supports deleting the resource, the response could include, for example, the URI to the author resource, because that's what's required for deleting it.
    3. Self-descriptive message - Each message must include enough information on how to process it. When a consumer requests data from an API, we send a request message, but that message also has headers and a body. If the request body contains a JSON representation of a resource, the message must specify that fact in its headers by including the media type, application/json for example. Like that, the correct parser can be invoked to process the request body, and it can be serialized into the correct class. Same goes for the other way around. Mind you this application/json media type is a simple sample. Media types actually play a very important role in REST.
    4. HATEOS (Hypermedia as the Engine of Application State) - This is the one that a lot of RESTful systems fail to implement. Remember that example we had in the beginning of the module when we explained while looking at a newspaper site how the state of our browser changed when we clicked the link? Well, that link, that's hypertext. Hypermedia is a generalization of this. It adds other types like music, images, etc., and it's that hypermedia that should be the engine of application state. In other words, it drives how to consume and use the API. It tells the consumer what it can do with the API. Can I delete this resource? Can I update it? How can I create it, and where can I find it? This really boils down to a self-documenting API, and that documentation can then be used by the client application to provide functionality to the user.

![REST Constraints 1](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/REST_Constraints_1.png?raw=true)

![REST Constraints 2](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/REST_Constraints_2.png?raw=true)

A system is only considered RESTful when it adheres to all the required constraints.

Not all of these constraints are straightforward to implement, and to be completely correct, an architecture that skips on one of the required constraints or weakens it is considered not conformed to REST, and that immediately means that most APIs that are built today and are called RESTful, well, they aren't really RESTful.

It does mean that you need to know the consequences of deviating from these constraints and understand the potential tradeoff.

HTTP APIs come in many forms.  It's called the Richardson Maturity Model, and it'll help us position REST for building HTTP APIs better.

## The Richardson Maturity Model

The Richardson Maturity Model is a model developed by Leonard Richardson.

It grades, if you will, APIs by their RESTful maturity, so it's interesting to look into it as it shows us how we can go from a simple API that doesn't really care about protocol nor standards, to an API that can be considered RESTful.

- Level 0 - The Swamp of POX, or plain-old XML - This level states that the implementing protocol, HTTP, is used for remote interaction. But, we use it just as that and we don't use anything else from the HTTP protocol correctly. So for example, to get some altered data, you send over a POST request to some basic entry point URI, like host/myapi, and in the body you send some XML that contains info on the data you want. You then get back the data you asked for in the response. To create an author, you send another POST request with some data in the body to that same entry point, and so on. In other words, HTTP is used, but only as a transport protocol. And example of this is SOAP, or other typical remote procedure call implementations. It's not a fully correct statement, but you see a lot of these RPC style implementations when building services with Windows Communication Foundation.

![The Richardson Maturity Model Level 0](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/The_Richardson_Maturity_Model_Level_0.png?raw=true)

- Level 1 - Resources - From this moment on, multiple URIs are used and each URI is mapped to a specific resource, so it extends on level 0 where there was only 1 URI. For example, we now have a URI, host/api/authors, to get a list of authors, and another one, host/api/authors, followed by an ID, to get the author with that specific ID. However, only one method like POST is still used for all interactions. So, the HTTP methods aren't used as they should be according to the standards. This is already one little part of the uniform interface constraint we see here. From a software design point of view, this means reducing complexity by working with different endpoints instead of one large service endpoint.

![The Richardson Maturity Model Level 1](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/The_Richardson_Maturity_Model_Level_1.png?raw=true)

- Level 2 - Verbs - To reach a level 2 API, the correct HTTP verbs, like GET, POST, PUT, PATCH, and DELETE, are used as they are intended by the protocol. In the example we see a GET request to get a list of authors, and a POST request containing a resource representation in the body for creating an author. The correct status codes are also included in this level, i. e. use a 200 Ok after a successful GET, a 201 Created after a successful POST, and so on.  This again adds to that uniform interface constraint. From a software design point of view, we have just removed unnecessary variation. We're using the same verbs to handle the same types of situations.

![The Richardson Maturity Model Level 2](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/The_Richardson_Maturity_Model_Level_2.png?raw=true)

- Level 3 - Hypermedia controls - This means that the API supports HATEOAS, another part of that uniform interface constraint. A sample GET request to the authors resource would then return not only the list of authors, but also links, hypermedia, that drive application state. From a software design point of view, this means we've introduced discoverability, self-documentation. What is important to know is that according to Roy Fielding, who coined the term REST, a level 3 API is a precondition to be able to talk about a RESTful API. So, this maturity model does not mean there's such a thing as a level 1 RESTful API, a level 2 RESTful API, and so on. It means that there are APIs of different levels, and only when we reach level 3 we can start talking about a RESTful API.

![The Richardson Maturity Model Level 3](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/The_Richardson_Maturity_Model_Level_3.png?raw=true)

## Positioning ASP.NET Core for Building RESTful APIs

RESTful APIs can be built with a variety of technologies. We're going to use ASP. NET Core and the MVC middleware.

It's very important to know that we don't just get a RESTful API out of the box just because we're building an API with ASP. NET Core. That's our responsibility. We get that by adhering to the constraints.

The rest of the explanation is on the API file.

## Structuring Our Outer Facing Contract

The outer facing contract consists of three big concepts a consumer of an API uses to interact with that API.

- Resource Identifier - the URIs where the resources can be found
- HTTP methods
- Payload (OPTIONAL) - Representation - Media types

 When creating a resource, the HTTP response will contain a resource representation in its body. The format of those representations is what media types are used for, like application JSON. The uniform interface constraint does cover the fact that resources are identified by URIs. Each resource has its own URI, but as far as naming of resources is concerned, there isn't a standard that describes that, or at least not unless you want to dive into OData. There are, however, best practices for this. A resource name in a URI should always be a noun. In other words, a RESTful URI should refer to a resource that is a thing, instead of referring to an action.

 So we shouldn't create a getauthors resource, that's an action. We should create an authors resource, that's a thing conveyed by a noun, and use the GET method to get it. To get one specific author then, we'd append it with a forward slash and the authorId. Using these nouns conveys meaning, it describes the resource, so we shouldn't call a resource orders, when it's in fact about authors. That principle should be followed throughout the API for predictability. It helps a consumer understand the API structure.

![Resource Naming Guideline 1](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Resource_Naming_Guideline.png?raw=true)

 If we have another non-hierarchical resource, say employees, we shouldn't name it api/something/something/employees, we should name it api/employees. A single employee then shouldn't be named id/employees, it should be named employees, forward slash, and the employeeId. This helps keep the API contract predictable and consistent. There's quite a bit of a debate going on on whether or not we should pluralize these nouns. I prefer to pluralize them as it helps to convey meaning. When I see an authors resource, that tells us it's a collection of authors and not one author, but good APIs that don't pluralize nouns exist as well. If you prefer that you can, but do make sure to stay consistent. Either all resources should be pluralized nouns, or singular nouns, and not a mix.

![Resource Naming Guideline 2](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Resource_Naming_Guideline_2.png?raw=true)

 Another important thing you'd want to represent in an API contract is the hierarchy. Our data or models have structure. For example, an author has books that should be represented in the API contract. So if you want to define an author's books, where the books in the model hierarchy are children of an author, we should represent them as api/authors/authorId/books. A single book should then be followed by the bookId. APIs often expose additional capabilities like filtering and ordering resources, those parameters should be passed through the credit string, they aren't resources in their own right. So we shouldn't write something along the lines of api/authors/orderby/name. There's a few contract smells in that URI. A plural noun should be followed by an Id, and not by another word, and orderby isn't a noun, and a URI like this would mean we'd have defined three different resources - authors, authors/orderby, and authors/orderby/name. So api/authors followed by orderby=name in the credit string is a better fit.

![Resource Naming Guideline 3](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Resource_Naming_Guideline_3.png?raw=true)

But there is an exception. Sometimes there's these remote procedure calls, style-like calls, like calculate total, that don't easily map to resources. Most RPC-style like calls do map to resources, as we've just proven, but what if we need to calculate, say, the total amount of pages an author wrote? It's not that easy to create a resource from that using pluralized nouns. You'd end up with something like api/authors/authorId/pagetotals. We'd expect this to return a collection and not a number.

![Resource Naming Guideline 4](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Resource_Naming_Guideline_4.png?raw=true)

Api/authors/authorId/totalamountofpages. It isn't according to these best practices, but as long as it's an exceptional case, it doesn't mean you've suddenly got a bad API. Remember there isn't standards for following for these naming guidelines, these are just guidelines. So by following these simple rules we'll end up with a good resource URI design.

![Resource Naming Guideline 5](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Resource_Naming_Guideline_5.png?raw=true)

Talking about IDs. REST stops at that outer facing contract. The layers underneath, including the data store, are of no importance to REST, so getting an author might mean that you're actually fetching data from three different data stores, including some fields from Active Directory, to compose that author resource representation. So it's of no importance, our resource isn't the same as what is in the backend store. These are two different concepts. But from that follows the question, what should we use as identifiers? REST is unrelated to the backend data store, yet often you'll see APIs that actually use the auto-numbered primary key Ids from the database. If the backend doesn't matter, what happens to the resource URIs if you change the backend? The resource URI should remain the same, but if resources are identified by their database auto-numbered fields, and we switch out our current SQL Server to a backend that uses another type of auto-number sequence like MongoDB, all of a sudden all our resource Ids can change. We can, of course, work around that on migration, but still, it's a good idea to keep this in mind when designing resource URIs. And there's a solution for this. GUIDs, unique and unguessable values you can use as primary key in every database. From that we can then switch out datastore technologies and our resource URIs will stay the same. We're also no longer potentially exposing implementation details, as those GUIDs don't give anything away about the underlying technology. This advantage, in my book, is readability. As a developer it's not that convenient to type over a GUID to test an API call, but that's what testing tools are for, and for the end product it shouldn't matter. It's not users like you and me who tend to talk to an API, say for during development, its other pieces of code. And for those it really doesn't matter if the URI contains a hard-to-type GUID.

![Resource Naming Guideline 6](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Resource_Naming_Guideline_6.png?raw=true)

![Resource Naming Guideline 7](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Resource_Naming_Guideline_7.png?raw=true)

## Routing

Routing matches a request URI to an action on a controller. So once we send an HTTP request, the MVC framework parses the URI, and tries to map it to an action on a controller, and there's two ways it can do this, convention-based or attribute-based.

- Convetion-based - we have to configure these conventions. We can do that by passing in these routing conventions to the UseMvc extension method. This example would map the URI values index to an index method on a controller named values controller. As you can guess from that example, this is a typical sample of something that's used when building a web application with views that return HTML. The MVC Middleware can be used for that, but for APIs the ASP. NET Core team recommends not using convention-based routing, but attribute- based routing instead.

![Routing Guidelines 1](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Routing_Guidelines.png?raw=true)

- Attribute-based - as the name implies, allows us to use attributes at controller and action level. We provide these with a URI template, and through that template and the attribute itself, a request is matched to a specific action on a controller. For this we use a variety of attributes, depending on the HTTP method we want to match. We should not dinamically put the Route of the Controller as the controller name, because if we were to have a refactoring of our codes, and rename the controller class to URI, to our authors resource, would automatically change. For APIs this in not an advantage, resource URIs should remain the same, and if we were to refactor this controller so it has another name, all our resources URIs would change. The name of the underlying class is of no importance to the consumer, so that is something we want to avoid.

![Routing Guidelines 2](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Routing_Guidelines_2.png?raw=true)

## Interacting with Resources Through HTTP Methods

Different actions can happen to resources at the same URI. For example, getting an author and deleting an author are interactions with the same resource URI. It's the method that defines the action that will happen, and depending on the method, we'll potentially need to send or get a payload. It's important to follow this standard so other components of our application can rely on this being implemented correctly.

- GET - Reading Resources - There is no request payload, but the response payload contains either a list of author representations, or a single author.

- POST - Creating a resource - Payload we pass in is a representation of the resource we're going to create, an author in our example, and the response payload then contains the newly created author resource.

- PUT or PATCH - Ppdating resources - two options are available. The first one is PUT, which should be used for full updates. A PUT request to api/authors/authorId would update the author with that Id. The request payload is a representation of the resource we want to update, including all fields, and if a field is missing, it should be put to its default value. The response payload can be that updated author, or it can be empty, but you don't always want to fully update your resource, in fact, more often than not, you'll need partial updates to update only one or two fields instead of all of them. And that's what the PATCH method is for. The URI is the same as for PUT. The request payload is somewhat special here, it's a JsonPatchDocument, essentially a set of changes that should be executed on that resource. And just as with PUT, the response payload can be that updated author, or it can be empty.

- DELETE - Delete a resource - This time, both requests and response payloads are empty.

- HEAD - Is identical to GET with the notable difference that the API shouldn't return a response body, so no response payload. It can be used to obtain information on the resource like testing it for validity, for example, to test if a resource exists.

- OPTIONS - Represents a request for information about the communication options available on that URI. So in other words, OPTIONS will tell us whether or not we can GET the resource, POST it, DELETE it and so on. These OPTIONS are typically in the response headers and not in the body, so no response payload.

![HTTP Methods](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/HTTP_Methods.png?raw=true)

## Outer Facing Model vs Entity

Outer Facing Model != Business Model != Entity Model

When we designed the outer facing contract, we learned that REST stops at that level. What lies underneath that outer facing contract is of no importance to REST. From that we already know that the entity model, in our case used by Entity Framework Core, as a means to represent database roles as objects, should be different from the outer facing model. In some application architectures there's a business layer in-between, which in turn is different from the outer facing model and the entity model. The outer facing model does only represents the resources that are sent over the wire in a specific format, but it also leads to possibilities. 

Take an author, for example. We can see some pseudocode for that. An author is stored in our database with a DateOfBirth, but that DateOfBirth, well that might not be what we want to offer up to the consumers of the API. They might be better off with the age. Another example might be concatenation. Concatenating the FirstName and LastName from an entity into one name field in the resource representation, and sometimes data might come from different places. An author could have a field, say, Royalties, that comes from another API our API must interact with. That alone leads to issues when using entity classes for the outer facing contract, as they don't contain that field.

![Outer Facing Model VS Entity Model](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Outer_Facing_Model_VS_Entity_Model.png?raw=true)

Keeping these models separate leads to more robust, reliably evolvable code. Imagine having to change a database table, that would lead to a change of the Entity class. If we're using that same Entity class to directly expose data via the API, our clients might run into problems because they're not expecting an additional renamed or removed field. So when developing, it's fairly important to keep these separate.

NOTE: Attributes can also be used in other classes, like our DTOs (not only Entities), and they're very useful for validation scenarios, but the point here is that we won't implement these on the AuthorDto class, because this one is only used for one purpose, returning data. So using validation attributes or data annotations used to validate input doesn't make sense on this AuthorDto class, which is only used for returning data to the consumer.

## Handling Faults

Server errors.

We get back a 500 Internal Server Error. ASP. NET Core will automatically return this when an unhandled exception happens.

We might want to execute some additional logic when an exception happens, like logging a custom message or some information for the system admins.

There's a few ways we can handle these exceptions:

- Try-Catch - We want to make sure a 500 Internal server error is returned when something happens that results in our API not being able to fulfill the request, like our random exception we're throwing. So when we catch this exception, we return a status code 500. For that we can use the status code helper method. We pass in the status code and an optional message. That message, an object actually, will be serialized into the response body. What's important here is that we don't want to pass in the exception itself, let alone the stack trace. Passing in the stack trace and sending it to the consumer of the API means we would be exposing implementation details to that consumer, and that's a security risk, but it's also useless. The consumer of the API has no use for that stack trace, as this is a fault an exception is not responsible for, and can't do anything about. So we return a generic message or no message at all, as consumers of the API are often machines rather than humans. This means we'd essentially have to write try-catch statements for each action, and maybe logging statements. Moreover, even though we're in the development environment, we no longer see the developer-friendly error page. That's because we handled the exception when we called it by returning that 500 Internal Server Error. When running in production, you don't want to return a stack trace, but when you're developing it can be very useful. There's another option.
- Global Exception Handling - what we saw before we wrote a try-catch statement, well that is global exception handling at work. Let's open the Configure method in the Startup class. There's two pieces of middleware here that handle these exceptions, the exception handler middleware is used when we're in a production environment. An approach I like to take to handle faults when building a RESTful API is to let the developer exception page as-is during development. That said, when not running in a development environment, I like to add a bit of configuration to the exception handler middleware to have it return a generic error message. So this is the exception handler middleware, and we can configure that by passing in a lambda that returns an action on IApplicationBuilder. We can then call Run on that appBuilder. What that will do is add a piece of code, which we're going to write, to the request response pipeline. So what we want to do is make sure that the status code is 500, and that we write out a generic error message as the response body. So let's write a piece of code. We need an action on the context here, and on that context we use context. Response. StatusCode to set the status code, and then we write out our error message by calling into context. Response. WriteAsync. That one is from the Microsoft. AspNetCore. Http namespace. We pass in that same generic error message and we're done.

## Parent/Child Relationships

Then there's that author entity. We could add an AuthorDto here, letting AutoMapper take care of the mapping for us, but in this case that would result in the same AuthorDto being returned for each book. That's redundant information, and it will hurt performance sending it over the wire for each book.  If you had that same author again, and again, for each book, while the author possibly includes a collection of books, we will run into circular reference errors, so we do not include the author here. We can, however, safely include the AuthorId.

let's set this off against one of our constraints, manipulation of resources through representations. That's the constraint that's stated that when a client altered a presentation of a resource, including any possible metadata, it must have enough information to modify or delete a resource on the server, provided it has permission to do so. o if the consumer of the API gets the response we see now, does he have enough information to modify or delete the author? Well, not really, what should be in the response to allow for that, at a minimum, is the resource URI. We already include an Id, and often that's considered enough. From the Id a consumer can create a URI, but if you think about this a bit further, it isn't completely correct. An Id alone isn't what identifies the resource, it's the URI that identifies the resource, and the resource URI is part of the request, but it's not part of the response. So to adhere to this constraint we should include the URI in each representation if update or delete is allowed. It's just a matter of adding an extra field and filling it up with the URI, but it's also not completely correct, and we're just getting started. There's a much better way of handling this than including the URI, and that's through HATEOAS.

## Method Safety and Method Idempotency

A method is considered safe when it doesn't change to resource representation. For example, get and head are considered safe. Mind you that doesn't mean that other types of manipulation can't happen as a result of performing a get request. Behind the outer-facing contracts, the API might update a fields in related tables, but the resource representation itself shouldn't change, and if those side effects happen, these weren't requested by the consumer of the API.

A method is considered idempotent when it can be called multiple times with the same result. In other words, the side effects of calling it once are the same side effects that happen when calling it multiple times.

Get is both safe and idempotent. It doesn't change the resource representation, and when you call it multiple times, the same results are returned. Options and head follow the same logic, but post is none of these, it causes changes in resource representations because it creates them, and if we call post multiple times, multiple resources should be created. Delete isn't safe, the resource representation changes as the resource is deleted, but it is idempotent. Deleting the same resource multiple times has the same result as deleting it once. Put for full updates isn't safe either. If you update the resource, its representation might change, but it is idempotent. Updating the same resource multiple times results in the same representation as updating it once. And lastly, patch for partial updates is neither safe nor idempotent. We learned that through patch it's easy to, for example, add items to an array, which results in different representations if you do that multiple times, and this is not just some theory, it helps us decide what we are allowed to do for each method. We're building a RESTful API that uses these HTTP methods. It's a standard, so we should correctly implement it so other components we might use can rely on this being correctly implemented.

![Method Safety and Method Idempotency](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Method_Safety_&_Method_Idempotency.png?raw=true)

## Creating a Resource

We should create different DTOs for different operations.

Mind you there are system where the properties on the class used for output are exactly the same as those on the class used for input, but even in those cases, I'd suggest to keep these separate. It leads to a model that's more in line with the API functionality, make change already factoring afterwards much easier, and when validation comes into play, you typically want validation on input, but not necessarily on output. So I suggest to use a separate Dto for creating, updating, and returning resources.

The first thing we need to check is if the input provided in the request body was correctly serialized to AuthorForCreationDto. If that isn't the case, the author value will be null, that means the client made a mistake, so we return a 400 BadRequest. If that checks out, we can map the AuthorForCreationDto to an author entity, and add it to the database finder repository. So we'll have to create a new mapping first.

This is the first time we are adding an item to the database, so this might need some additional explanation. At this moment, the entity hasn't been added to the database yet, it's been added to the DbContext, which represents a session with the database. To persist our changes, we must call save on the repository, and if that's save fails, we should return a 500 internal server error. This save methods returns a Boolean, true or false, and we should see the repository has a bit of a black box. In essence, the controller doesn't know about the implementation, so it might contain exception handling code, it might not, it all depends on the provided implementation. So, I do like to treat it as a black box, but we do know what we want to return, a 500 internal server error with a generic error message. We only return a generic error message, because the consumer of the API really doesn't need to know what exactly went wrong, it just needs to know that it's not its responsibility. Our generic exception handler will not catch this, as save doesn't throw an exception, but we can use the StatusCode method for that, passing in the StatusCode. The StatusCode is 500, and we provide a generic message. But we already configured the exception handler middleware in the previous module to return a 500 internal server error with a generic message if an unhandled exception occurs. So another option is to throw an exception from the controller, and let the middleware handle it. Now is this a good approach or a bad approach? Well it's, it kind of depends. Throwing exceptions is expensive, it's a performance hit, so that would lead us to returning the StatusCode from the controller as a best practice, but on the other hand, that also means that we'll have code to return 500 internal server errors in different places, on the global level and in the controller itself. At this moment, that's not too much of a problem, but once we start implementing logging, that would also mean we'd want to provide logging code on each StatusCode 500 we return. I've seen both approaches, and there's something to be said for both. In this case, we're going to have the middleware handle all our responses that warrant a 500 internal server error. That'll come in nicely when we need to implement logging for these types of StatusCodes later on. We'll only have to write that code in one place, being at the configuration of the exception handler middleware.

In case of a successful post, we should return a 201 created response. For that, we can use the CreatedAtRoute method. This method allows us to return a response with the location header, and that location header, that will then contain the URI where the newly created author can be found. So the first thing we need to pass into this method is the route name that's going to be used for generating the URI. it should refer to the action to get a single author, that's our GetAuthor action. So, let's give this a name we can refer to, say GetAuthor, there we go. And we pass in GetAuthor as the first parameter of our CreatedAtRoute method. Now to get an author, we need the author ID. We need to pass that in as a route value, so the correct URI can be generated containing that ID. To do that, we pass in an anonymous type, and we give that anonymous type one field, ID, which is the name used in our route template, and we give it a value of authorToReturn. Id. And lastly, we want to pass in the actual authorToReturn Dto. This one will get serialized into the response body.

 Let's send it, we get back a 201 created, so that means that our author has been created again. We've now got two James Ellroy's in our database. Let's get that list of authors, and here we see that James Ellroy has been added to our database twice. So post is not idempotent, we cannot send that same request twice, and have the same result as only sending it once, because we now have two authors instead of just one. So now you've got a bit of unnecessary data in our database.

## Creating a Child Resource

There's already an AuthorId in the URI, so if we allow the AuthorId in the payloads, well, we might end up with an issue we want to avoid, and that issue is that a post to the book's resource for author A would create a book for author B.

There's two ways to tackle this issue. One is adding the AuthorId in the BookForCreationDto. But that would mean we have to check if that AuthorId matches the AuthorId from the URI. The second approach is not sending over the AuthorId in the request body, and thus not adding it as a property on our BookForCreationDto.

Everytime we do a create we should return an CreatedAtRoute("GetBookForAuthor", new { authorId = authorId, bookId = bookToReturn.Id }, bookToReturn);

## Create Child Resources Together with a Parent Resource

There's a pretty common use case when working with RESTful APIs, creating a list of children together with a parent resource in one go. So rather than having to send subsequent requests to the book's resource for an author, we want to create the author and its list of books all together.

We're going to extend this, so it can create both an author without books and one with books. The first thing we need to do is extend the AuthorForCreationDto. Here we'll want to add a collection of books, and we already have a Dto for creating books, our BookForCreationDto. We must use that class and not the BookDto to avoid running into issues like having a book with the wrong AuthorId. It's a good idea to always initialize these types of collections as to avoid null reference exceptions.

What's going to happen when we pass in an author with a list of books is that the author will be de-serialized into an AuthorForCreationDto, which contains a list of BookForCreationDto objects, so all the books in our request payload will be de-serialized into a list of that type. The checks here can remain as is, and the actual persistence logic is in the repository. With this little change, we've now got an action that we can use to create one author with or without books. This again drives home the point of differentiating between input and output. Input, in this case, is an author with optionally a list of books, an output is just the author, but with different fields. Now of course the repository must support this as well. We're treating it as a bit of a black box, because we're focusing our rests, and that stops at the outer-facing layer.

he AddAuthor method on the repository will look at the books, and it will generate an Id for each of them if there are books in the collection. From that moment on, the DbContext contains a new author and a new list of books for that author, and once we call save, these are persisted, and that's handled by Entity Framework Core. Were we to make the Ids for each entity an identity column, EF Core will automatically generate GUIDs for us, so we don't even have to write code to create them. Our request payload was de-serialized into an author with two books. Let's continue. After mapping, our authorEntity also has these two books, as the authorEntity also has a collection of books, the AuthorForCreationDto has been mapped to an authorEntity, and the BookForCreationDto has been mapped to bookEntities, but the entities don't have their Ids filled out yet, and the author is also null at the moment. After they go to the repository, the Ids will be filled out.

## Creating a Collection of Resources

We can't just post a list of authors to api/authors, as how we named URI implies that one author will be posted, so what can we do? This is one of the reasons it's so important to realize there doesn't have to be a direct mapping between the entities in the backend store and our outer-facing contract. It's not because the author's resource maps to the authors in our database, that other resources cannot have an effect on the same database table, so what does all of this mean, and what should we do then? Well, we design a new resource, an AuthorCollections resource. This requires a request body that's an array of all representations, and it will result in a list of authors being added to the database.

As we're creating a resource, this warrants a 201 Created status code. So we should return CreatedAtRoute, and this is the tricky part. We should include the URI where we can get this collection in the location header, so we need an action to get this AuthorCollection, but how do we do that then? If we're adding to the authors to the author table via our data store, so we don't have a direct key for an AuthorCollection. The key is a combination of a set of AuthorIds, the separation between the outer-facing contract and the data store is becoming even more apparent.

## Working with Array Keys and Composite Keys

Let's first separate these two. With an array key, I mean that the key is a comma-separated list, something along the lines of 1, 2, 3, if we were working with integer keys. With a composite key, I mean the key is a combination of key-value pairs, something along the lines of key 1=1, key 2=2, and so on.

the question is more related to URI design than implementation. We need an array key for our location header when creating an AuthorCollection, so let's start with that. There isn't a standard that states how to work with this, but what's often done is using round brackets containing the comma-separated key. So, let's add a method to get one AuthorCollection that accepts a key that is actually a list of author GUIDs. (key1,key2,key3)

hat id's parameter is an array of GUID, but there's no implicit binding to such an array that's part of the route. But if there's no implicit binding, well, we'll just have to provide it ourselves, and we can do that with the help of a custom-model binder. So let's add one to the Helpers folder, ArrayModelBinder sounds like a good name.

```csharp
public class ArrayModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // Our binder works only on enumerable types
        if (!bindingContext.ModelMetadata.IsEnumerableType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        // Get the inputted value through the value provider
        var value = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName).ToString();

        // If that value is null or whitespace, we return null
        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        // The value isn't null or whitespace, 
        // and the type of the model is enumerable. 
        // Get the enumerable's type, and a converter 
        var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
        var converter = TypeDescriptor.GetConverter(elementType);

        // Convert each item in the value list to the enumerable type
        var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => converter.ConvertFromString(x.Trim()))
            .ToArray();

        // Create an array of that type, and set it as the Model value 
        var typedValues = Array.CreateInstance(elementType, values.Length);
        values.CopyTo(typedValues, 0);
        bindingContext.Model = typedValues;

        // return a successful result, passing in the Model 
        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
        return Task.CompletedTask;
    }
}
```

So any ModelBinder should implement IModelBinder. And that interface exposes exactly one method, BindModelAsync. We get a model bindingContext passed in, and through that model bindingContext, we can get information of what we're binding to, and we can get the input values. So the first thing we do is make sure that the metadata about the model tells us that it's an enumerable type. If not, we set the result on the bindingContext to ModelBindingResult. failed, and we return a completed task signifying that this part is done, otherwise we can continue. The bindingContext also exposes a value provider. Through that, we can get the inputted value by passing in the model name on the getValue method. That value will, in our case, then contain a string that's a list of GUIDs. If the value IsNullOrWhiteSpace, we want to return null, that's what we use to check for a bad request. If the checks we just did check out, we can continue. We look for the generic type argument on our model type, that should return GUID. And then we create a converter. These converters are built in, and they help us with converting, in our case, string values to GUIDs. To get that converter, we call into GetConverter on the type descriptor class, passing in the elementType, GUID in our case. After that, we run through our list of values. So we split it up with a comma as a separator, and each string is converted to a GUID by calling into Converter. ConvertFromString. Lastly, we call to array, so values now contains an array of GUID. The last thing to do is create an actual result for our bindingContext. This should contain an IEnumerable of GUID. So we create an instance of that by calling into Array. CreateInstance. This creates an array of elementType for a specific length. ElementType is GUID, and values. Length, well, that's the amount of GUIDs we passed in. Then we copy over the values array into our typedValues array, and we set the typedValues as the model on our bindingContext. At this moment, our binding was successful. So, we set the result to success, and we pass in the bindingContext. Model. Lastly, we return Task. CompletedTask.

Let's make sure we can use our custom model binder to bind the Ids from the URI to our enumerable of GUID. We can do that with the ModelBinder attribute, passing in the BinderType. That's our ArrayModelBinder.

We need to be able to refer to the route from our method that creates an author collection, so we can create URI for the location header. So let's give it a name, say getAuthorCollection.

To correctly create a response, we'll need the content for the response body, an IEnumerable of AuthorDto. So let's map the AuthorEntities to that. But also we're going to need something extra, a list of Ids, as that's our key. For that, we can use a string. join statement, passing in a comma as a separator. From each author in our collection to return, we then select the ID.

That takes care of that, but what about composite keys? A composite key consists of multiple key value pairs, instead of a list of one field. So it could be something along the lines of key 1 equals value 1, key 2 equals value 2. This is a requirement that often comes up in systems where there is no simple one-on-one mapping between the outer-facing contract and the backing data store. We haven't got a good example of such a resource, but we already know how to implement something like this. We should use a route template with two keys, in this case, that map to two parameters in the action signature. So, there's actually nothing new about that, say for designing the resource URI.

```csharp
[HttpPost]
public IActionResult CreateAuthorCollection([FromBody] IEnumerable<AuthorForCreationDto> authorCollection)
{
    if (authorCollection == null)
    {
        return BadRequest();
    }

    var authorEntities = _mapper.Map<IEnumerable<Author>>(authorCollection);

    foreach (var author in authorEntities)
    {
        _libraryRepository.AddAuthor(author);
    }

    if (!_libraryRepository.Save())
    {
        throw new Exception("Creating an author collection failed on save");
    }

    var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

    var idsAsString = string.Join(",",
        authorCollectionToReturn.Select(a => a.Id));

    return CreatedAtRoute("GetAuthorCollection",
        new { ids = idsAsString },
        authorCollectionToReturn);
}

[HttpGet("({ids})", Name = "GetAuthorCollection")]
public IActionResult GetAuthorCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
{
    if (ids == null)
    {
        return BadRequest();
    }

    var authorEntities = _libraryRepository.GetAuthors(ids);

    if (ids.Count() != authorEntities.Count())
    {
        return NotFound();
    }

    var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

    return Ok(authorsToReturn);
}
```

## Handling POST to a Single Resource

We've always posted to collection resources up until now, one author to the author's resource, but what about posting to a single resource instead of to a collection resource? Posting to a URI like this can never result in a successful request. It should either return a 404 Not Found if the author doesn't exist, or a 409 conflict if the author already exists. Let's imagine we'd allow posting to a single resource URI that contains an Id of an nonexisting author. While post is used for creating resources, the Id part of the URI signifies a mistake. It's the server that's responsible for creating the resource URI and not the consumer of the API. If we allow the consumer to generate the URI, a post like this would have to be idempotent, and posts isn't idempotent, we cannot rely on the fact that multiple post requests will result in the same outcome. So if treating post as idempotent can be avoided, it should. In other words, a 404 is warranted. Say we send a request like this to an existing URI, we'd expect a 409 conflict as we're trying to create your resource that already exists. Let's try this, and we also get back a 404 Not Found. It's still the same type of URI not linked to a route template, so this makes sense, but it's not correct. It's a matter adhering to the HTTP standard. Most APIs would not bother with adding an additional action just to return a correct HTTP StatusCode.

ere we're going to add a new action, BlockAuthorCreation. We'll again use the HttpPost attribute, this time adding Id to the route template. We then accept that Id in the parameter list. We don't have to bother with accepting a specific type of parameter to serialize the request body to. We're not going to use it to effectively add an author as that's not allowed, we're only going to return the correct StatusCodes. We use this Id to check if an author with that Id exists. If it does, we should return a 409 conflict. There isn't a convenient helper method for that like OK and not found, but we can use the StatusCodeResult class for that. StatusCodeResult is an action result that results in a response without a body, but with a specific StatusCode. So we new that up, and pass in Satus409Conflict from the StatusCode enumeration. If the author doesn't exist, we return not found.

```csharp
[HttpPost("{id}")]
public IActionResult BlockAuthorCreation(Guid id)
{
    if (_libraryRepository.AuthorExists(id))
    {
        return new StatusCodeResult(StatusCodes.Status409Conflict);
    }

    return NotFound();
}
```

We've been working with post in the last few demos, and we've used a content-type header to signify the media type of the request body. Let's open a post request again, and let's look at those headers, here's that content-type header. What would happen if we didn't provide a content-type header. Let's give that a try, let me uncheck that, and let's send. We get back 415 Unsupported Media Type. We didn't pass in the media type, so our API doesn't know how to handle this. In other words, we always need to provide a content-type header when providing a request body that's in line with the self-descriptive message constraint. But what if you want to support other types of input like XML, especially when you're working on an API that integrates between different systems, this can be a requirement.

## Supporting Additional Content-type Values and Input Formatters

 We added an additional output formatter in the previous module by manipulating the output formatters collection. There's another collection InputFormatters, which as the name implies, allows us to add a new input formatter. Let's add one for XML. What we're looking for is the XmlDataContractSerializerInputFormatter. In case you're wondering why we're using the XMLDataContractSerializer versions of the formatters, well, that's because this formatter supports types like the dateTime offset value we have for dateOfBirth. The XmlSerializer requires that the type be designed in a specific way in order to serialize completely. Most types in. NET were not designed with the XmlSerializer in mind, so that's why it's preferable to use the dataContractSerializer versions of it.

```csharp
services.AddMvc(setupAction => 
{
    setupAction.ReturnHttpNotAcceptable = true;
    setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
    setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
});
```

## Deleting a Resource

There's no response body after the successful delete to resources gone, so we return a 204 No Content. This signifies that the request was successful, but doesn't have a response body.

Delete is obviously unsafe, as it changes the resource, in this case it deletes it. It's also idempotent, sending the same delete request over and over again will not change the resources any more than it did with the first request the first time it was deleted.

## Deleting a Resource with Child Resources

We'll see how we can handling deleting an author. When we do that, the books for that author should be deleted as well, as an author is the parent resource of a book's resource.

First thing to check, does the author exist? We don't use the AuthorExists method, because we need that author entity to pass into the repository to delete it. So we call GetAuthor on the repository, passing in the Id. If it's not found, we return Not Found. If that checks out, we call into the DeleteAuthor method on the repository. We pass in authorFromRepo. CascadeOnDelete is on by default, so when we delete an author with Entity Framework Core, the books for that author are deleted as well. To effectively execute this statement, we call Save on the repository, and throw an exception if the save fails. Lastly, we return NoContent. Let's give that a try. We'll send a delete request to the author Stephen King, and we get back a 204 No Content. So the resource is gone and so are all the books, there's no way to get them anymore.

## Deleting Collection Resources

There's one case we didn't cover, sending a delete request to a collection resource. There's nothing that would stop us from doing so, so it's perfectly allowed. A resource is identified by a URI, and that URI can refer to a collection resource or a single resource, but it's still, well, just a resource, but what would that do? Say we send a delete request to api/authors. That would mean we'd have to delete all our authors, and as books are children of authors, the books for all those authors are well. In other words, we'd end up with not a single resource left to get. So while supporting this is allowed, it's advised against, because delete is a pretty destructive action. Unless you really need it, you don't want to allow this on a collection resource, as that might have the effect of thousands of resources being deleted in one go. Note that that's different from what we just did in the previous demo, where we deleted the books for an author when the author was deleted. In that case, we still send the request to a single resource, which happened to result in other resources getting deleted, because they don't make sense without that parent resource.

![Deleting Collection Resources](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Deleting_Collection_Resources.png?raw=true)

## Updating a Resource

We also learned that as put is for full updates, when a value for a specific field isn't passed in, it should be put to its default value. The approach we're taking here makes sure of that. If a put request is sent without a description, the description string will have its default value. But what about an Id? We're updating an existing resource, so we know the Id. Shouldn't we be able to pass it in then through the request body/ Well, we could, but let's look back at the URI of our resource. That already contains the Id of the book. Adding the Id in the request body is thus redundant information, and it will also mean we'd have to add an additional check. The Id in the URI should be the same as the Id in the request body to avoid allowing requests to one resource that actually updates another one. If they are not the same, the Id in the URI wins, and the exact same goes for the AuthorId, that as well is in the URI. So what I prefer to do is avoid these mistakes as soon as we can by not allowing the Ids that are already in the URI in the request body. That then brings us to another issue, if we want to call it that. Let's open the Dto for Creation next to the one for Update. Our BookForUpdateDto and our BookForCreationDto now contain the exact same properties. Can't we just reuse that then? Well, currently we could, but there's a conceptual difference between these Dtos reflected by the name. The one is for updating and the other one for creating, and even though the fields will stay the same in a lot of cases, what is allowed at an update isn't necessarily the same as what is allowed at creation.

Important here is that in REST we are updating the resource and not the entity. So while it might look tempting to just take the input and copy the field values over to this BookForAuthorFromRepo entity, we must keep in mind that projections might have to happen. So that actually means we should first map the entity to BookForUpdateDto, then apply the updated field values to that Dto, and then map the BookForUpdateDto back to an entity. The nice thing is that we're using AutoMapper, and it allows us to combine these steps.

1. Map
2. Apply Update
3. Map back to entity

why is this method empty in our implementation? Well in Entity Framework Core, these entities are tracked by the context. So by executing that Mapper. Map statement, the entity has changed to a modified state, and executing a save will write the changes to the database. So let's call into the save method on the repository.

Then we'll need to return something. For a successful update, a 200 OK could be returned, containing the updated resource representation in the response body. Alternatively, we could return a 204 No Content, both are valid. These days I tend to return a 204 No Content to avoid sending over data that isn't required by the consumer of the API. This approach leaves it up to the consumer to decide on this, but it's not a good case for all APIs. Sometimes fields can be updated that weren't part of the request. Think about a modified at timestamp that's returned when getting a resource. As learned when talking about method safety, put can have side effects like this. If that's the case, a 200 OK response containing the updated resource representation in the body seems more appropriate, but in the end, both are valid.

By the way, this does lead to the fact that put is used less and less these days. Imagine a resource with 30 fields, it's not that good for performance that a consumer of an API should have to send over all these field values when he just wants to change one. That's why patch for partial updates is often the preferred option.

## Repository Pattern

An abstraction that reduces complexity and aims to make the code safe for the repository implementation, persistence ignorant. So, from that we know there's a few good reasons to use the repository pattern.

- Less code duplication - Writing code to access a backing data stored directly in a controller action easily leads to code duplication. We might need to access authors or books from multiple parts in our application, so it's preferable to write that code just once instead of in every action or part of the application.
- Less error-prone code - Code will also become less error prone, if only for avoiding that application.
- Better testability of the consuming class - if you want to test the controller action, but the action also contains logic related to persistence, it's harder to pinpoint why something might go wrong. Is it logic in the action, or is it a persistence-related code in the action that fails? If there's a way to mock the persistence-related code and test again, you know that the mistake isn't related to that persistence logic. So using the repository pattern allows for this mocking of persistence logic, which means better testability of the consuming class.

Sometimes it's said that through a repository, you can switch out the persistence technology when needed, and while that is strictly speaking true, it's not really the purpose of the repository pattern. What it is very useful for, however, is allowing us to choose which persistence technology to use for a specific method on the repository. Getting a book might be easier through Entity Framework, getting an author with some complex logic might be more advisable through ado. net, or you might even call into an external service. For consumers of the repository like our controllers, it's of no interest what goes on in the implementation, rather than switching out one persistence technology for another for the complete repository, it allows us to choose the technology that's the best fit for a specific method on the repository, thus persistence ignorant.

Let's get back to why we're calling into that empty update method on the repository. We're working on a repository contract, not an implementation. Different implementations of that repository contract could exist. There's often a mocked repository implementation of a contract used for testing. For that, an update might actually have to do something. So that's why I'd suggest to always have this update method on your contract if an update is allowed. Even if in the specific implementation we're using, it doesn't do anything. By calling into it from the controller, we ensure that older implementations will still execute as expected. If we wouldn't call into this method, because we don't need it for our repository implementation, we might run into issues with other implementations like a mock implementation for testing.

![Repository Pattern](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Repository_Pattern_2.png?raw=true)

## Updating Collection Resources

As we remember from when we talked about deleting a collection resource in the previous module, a resource is just a resource. Single or collection, there's nothing that forbids an update. Say we send such a request to api/authors/ an AuthorId/ and books. That means we'd be updating the books resource to a new value, in this case a set of books. We are replacing the current set of books with a new set of books. Put is for full updates after all. The request body replaces what's at the URI the put request is sent to, which means that the correct way to handle this is to delete all the previous books for that author, and then create the list of books that's inputted for that author, so we end up with a completely-new books resource. Very important here is the distinction between what we're actually doing and what the side effects could be. We are still updating a resource, books, so put is warranted. The fact that some book resources are deleted, and others are created when sending that put request is just a side effect. So the reasoning is a bit the same as for deleting collection resources. It's allowed, but in general it's advised against, because it can be quite destructive. The full list of books must be replaced. We won't implement this in our API, but if you need to, you can. Just be aware of the potential consequences.

## Upserting

Create a new resource with put or patch instead of post. it requires a bit of explanation, because it's often misinterpreted and can cause quite a bit of confusion. We've got a consumer of our API on the left, and the API server on the right. In a lot of systems, it's the server that's responsible for creating the identifier of a resource. Part of it is often the underlying key in the data store, an integer value or a GUID, for example. A consumer might post a new author to the authors resource, and the server response with that newly-created author in the response body, and the location of the author resource, which the server generated in the location header. In fact, in most systems, the server decides on the resource URI, but REST does not have this as a requirement. It's perfectly valid to have a system where the consumer can do this, or where it's allowed for both the consumer and the server. Now if the key in the database is part of the resource URI, this doesn't just work with AutoNum fields. It does, however, when working with GUIDs. So yeah, you got me, this is one of the reasons I chose GUIDs instead of ints to store data in the backend store. Now let's think about what could happen. Say the server is responsible for creating the resource URI, and we want to send an update request. We need to get the URI to a resource from the server to be able to update it. The resource must already exist. And if it doesn't, we must return a 404 Not Found. Now let's imagine the consumer is also allowed to create resource URIs. In that case, we no longer need to get the URI from the server. The hard requirement for the resource having to have been created before vanishes. We can now send the put request to a previously nonexisting resource identifier that is valid, because the consumer is allowed to create it. In that case, that resource must be created when sending the put request, or in a way it's updated from being empty. So if the server is responsible for the resource identifiers, we must use post to create resources. We can't not know the URI of the resource in advance, but if the consumer of the API is allowed to create the resource identifier, well we can use put as well, and this is called upserting. Now let's think back at method idempotency to see if this still fits. We learned that post isn't idempotent, sending the same request more than once will result in different outcomes. Put, however, is, and that fits. If the consumer of the API chooses the URI, sending the request once will create a resource. Sending it again after that will have the exact same result. The same resource with the same Id is created.

