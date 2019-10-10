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


 If we have another non-hierarchical resource, say employees, we shouldn't name it api/something/something/employees, we should name it api/employees. A single employee then shouldn't be named id/employees, it should be named employees, forward slash, and the employeeId. This helps keep the API contract predictable and consistent. There's quite a bit of a debate going on on whether or not we should pluralize these nouns. I prefer to pluralize them as it helps to convey meaning. When I see an authors resource, that tells us it's a collection of authors and not one author, but good APIs that don't pluralize nouns exist as well. If you prefer that you can, but do make sure to stay consistent. Either all resources should be pluralized nouns, or singular nouns, and not a mix.


 Another important thing you'd want to represent in an API contract is the hierarchy. Our data or models have structure. For example, an author has books that should be represented in the API contract. So if you want to define an author's books, where the books in the model hierarchy are children of an author, we should represent them as api/authors/authorId/books. A single book should then be followed by the bookId. APIs often expose additional capabilities like filtering and ordering resources, those parameters should be passed through the credit string, they aren't resources in their own right. So we shouldn't write something along the lines of api/authors/orderby/name. There's a few contract smells in that URI. A plural noun should be followed by an Id, and not by another word, and orderby isn't a noun, and a URI like this would mean we'd have defined three different resources - authors, authors/orderby, and authors/orderby/name. So api/authors followed by orderby=name in the credit string is a better fit.


But there is an exception. Sometimes there's these remote procedure calls, style-like calls, like calculate total, that don't easily map to resources. Most RPC-style like calls do map to resources, as we've just proven, but what if we need to calculate, say, the total amount of pages an author wrote? It's not that easy to create a resource from that using pluralized nouns. You'd end up with something like api/authors/authorId/pagetotals. We'd expect this to return a collection and not a number. 

Api/authors/authorId/totalamountofpages. It isn't according to these best practices, but as long as it's an exceptional case, it doesn't mean you've suddenly got a bad API. Remember there isn't standards for following for these naming guidelines, these are just guidelines. So by following these simple rules we'll end up with a good resource URI design.

Talking about IDs. REST stops at that outer facing contract. The layers underneath, including the data store, are of no importance to REST, so getting an author might mean that you're actually fetching data from three different data stores, including some fields from Active Directory, to compose that author resource representation. So it's of no importance, our resource isn't the same as what is in the backend store. These are two different concepts. But from that follows the question, what should we use as identifiers? REST is unrelated to the backend data store, yet often you'll see APIs that actually use the auto-numbered primary key Ids from the database. If the backend doesn't matter, what happens to the resource URIs if you change the backend? The resource URI should remain the same, but if resources are identified by their database auto-numbered fields, and we switch out our current SQL Server to a backend that uses another type of auto-number sequence like MongoDB, all of a sudden all our resource Ids can change. We can, of course, work around that on migration, but still, it's a good idea to keep this in mind when designing resource URIs. And there's a solution for this. GUIDs, unique and unguessable values you can use as primary key in every database. From that we can then switch out datastore technologies and our resource URIs will stay the same. We're also no longer potentially exposing implementation details, as those GUIDs don't give anything away about the underlying technology. This advantage, in my book, is readability. As a developer it's not that convenient to type over a GUID to test an API call, but that's what testing tools are for, and for the end product it shouldn't matter. It's not users like you and me who tend to talk to an API, say for during development, its other pieces of code. And for those it really doesn't matter if the URI contains a hard-to-type GUID.

