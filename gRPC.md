# gRPC

## What are gRPC

A high-performance, open-source universal RPC framework.

- HTTP/2 - Transport
- protobuf serialization (pluggable) - The messages that we serialize, both for the request and response, are encoded with Protocol buffers. It is flexible to use JSON.
- Clients open one long-lived connection to a GRPC server
  - A new HTTP/2 stream for each RPC call
  - Allows simultaneous in-flight RPC calls
- Allow client-side and server-side streaming
- Backed by CNCF
- - GRPC was originally pioneered by a team at Google
- Next generation version of an internal Google project called "stubby"
- Now a F/OSS project with a completely open spec and contributors from many companies
  - Development is still primarly executed by Google devs

There are three high-performance event loop driven implementations:

- C
- Java
- Go
- The other implementations are all mapped to the C-Core (bindings to that)

GRPC Timeouts are just a parameter, it is supported on all RPC calls built in as part of the system. The client library knows about timeouts and we can just pass them in and it knows when to basically return to us and say that the call failed. In GRPC when a call times out it actually closes the stream that that RPC was open on so that the remote side, the server side, actually knows to cancel that request which means we can do really interesting things like if ou services way backed up and has not even gotten to serving a request it probably won't even see it because the GRPC layer will cancel it so it never actually gets handled which avoids from thinking we have stale services but we never had any logic to kill them.

Logging + Metrics - GRPC has interceptors that are basically a middleware, it is the same idea as HTTP context or any sort of HTTP framework, it is the same idea except that we can put them both on the client side and the server side. Most of the times in HTTP we do not do client side and GRPC has this already built-in.

Call status - Server-Side Streaming - HTTP does not have great support for the streaming of API but GRPC does. The idea is that we can subscribe to a resource and get streaming updates.

Bi-directional streaming API - how to list things, we can list a million resources. The first message that we send across the client can stream messages as well as the server sido so the idea would be that the client streams first a single list req thas has a filter and says "these are the records I am interested in" and then the server starts streaming call objects and at some point it stops and then the client says "give me more". We just create a single stream and stream until the client is done.

gRPC (gRPC Remote Procedure Calls) is an open source remote procedure call (RPC) system initially developed at Google. It uses HTTP/2 for transport, Protocol Buffers as the interface description language, and provides features such as authentication, bidirectional streaming and flow control, blocking or nonblocking bindings, and cancellation and timeouts. It generates cross-platform client and server bindings for many languages. Most common usage scenarios include connecting services in microservices style architecture and connect mobile devices, browser clients to backend services.

<https://en.wikipedia.org/wiki/GRPC>

<https://www.slideshare.net/JamesNK/grpc-on-net-core-ndc-sydney-2019>

<https://www.youtube.com/watch?v=RoXT_Rkg8LA>

<https://www.youtube.com/watch?v=fMq3IpPE3TU>

<https://www.youtube.com/watch?v=hNFM2pDGwKI>

<http://www.http2demo.io/>

<https://grpc.io/docs/tutorials/basic/csharp/>

<https://medium.com/@bimeshde/grpc-vs-rest-performance-simplified-fd35d01bbd4>

<https://medium.com/red-crane/grpc-and-why-it-can-save-you-development-time-436168fd0cbc>

<https://blog.usejournal.com/migrating-your-rest-apis-to-http-2-why-and-how-8caee7d798fb>

For a lot of services and requests

### Resiliency

- Retries - We can do retries in interceptors, kind of a middleware, and we can configure how our client should be behaving if the server is not responding and we can do that just through configuration and it is very transparent to the application. A proposal that has been made is Retry Hedging (speculative retries or backup retries) that is an advanced way of retries that we send the first request and after a certain delay if we do not get a response we send the second request and after a delay we send the third request, basically just fanning out our request to all the backends until we get some response and as soon as we get the response everything is canceled, every RPC is canceled. This helps us cut the tail latency drastically in certain cases. The downside, of course, is that we are going to spend more resources on GRPC and that is a really great way to create cascading failures and what we end up with is kind of a thundering herd kind of situation where everybody hammers the same back end that is already down and it is really hard to get out of it. To prevent those cascading failures GRPC has other features that help us:
- Retry Throttling and pushback - they are part of the same proposal and they are 2 ways of preventing cascading failures. Retry Throttlinh basically means we have a bucket of retries we can use and if they are used up we can not use anymore so the hedging won't issue RPCs if we used our retry bucket program. Pushback is on the server side so the server can actually signal to clients to hold on and send a request in set amount of time again and try it again.
- Deadlines - Instead of setting timeouts manually we can define a deadline on a request and the request will be automatically cancelled once the deadline runs out.

## Strengths

- Lookaside Load Balancing - it has the benefits of our client-side load balancing but the logic of load balancing a client we extract that into a central load balancer that is not part of the data plane, so we are not gonna have that extra hop, the load balancer sits to the side and basically just feed the client with updated lists of healthy backends upstreams, at the same time it also monitors the backend tendencies which services and endpoints are healthy or not. That is also the perfect place where we can introduce advanced routing or rollout strategies. We just extract logic from the client, from the data plane into a central place it only looks very much like a service mesh because what the lookaside load balance really is just a control plane for the GRPC clients. Proxyless RPC Mesh.

The main benefits of gRPC are:

- Modern, high-performance, lightweight RPC framework.
- Contract-first API development, using Protocol Buffers by default, allowing for language agnostic implementations.
- Tooling available for many languages to generate strongly-typed servers and clients.
- Supports client, server, and bi-directional streaming calls.
- Reduced network usage with Protobuf binary serialization.

These benefits make gRPC ideal for:

- Lightweight microservices where efficiency is critical.
- Polyglot systems where multiple languages are required for development.
- Point-to-point real-time services that need to handle streaming requests or responses.

## Weaknesses

- Load Balancing - we basically run our RPCs over a single long-lived connection so it means that we need some sort of load balancer to take those RPCs and pull each stream out individually and then maintain back-end connections.
- Error handling is really bad
- No support for browser JS
- Breaking API changes
- Poor documentation for some languages
- No standardization across languages

## gRPC vs APIs

<https://docs.microsoft.com/en-us/aspnet/core/grpc/comparison?view=aspnetcore-3.0>

"REST APIs are the worst APIs, except for all of the other ones"

### Why REST APIs are great

- Easy to understand (text protocol)
- Web infrastructure already built on top of HTTP
- Great tooling for testing, inspection, modification
- Loose coupling between clients/server makes changes (relatively) easy
- High-quality HTTP implementation in every language

### Why REST APIs suck

- No formal (machine-readable) API contract
  - Collary: writing client libraries requires humans
    - Collary: humans are expensive and do not like writing client libraries
- Streaming is difficult (night-impossible in some languages)
- Bi-directional streaming is not possible at all
- Operations are difficult to model (e.g. 'restart the machine')
- Inefficient (textual representations are not optional for networks)
- Your internal services are not RESTful anyways, they are just HTTP endpoints
- Hard to get many resources in a single request (GraphQL)

GRPC solves all those things

We create an GRPC IDL (Interface Definition Language), this fomal definition can be machine read which means we can run it through a compiler and so there is a compiler for GRPC and now we have client libraries generated for 9 different languages.

We have servers stubs generated for that API in 7 languages.

## Differences between GRPC and SOAP/WSDL

Machine-readable API contracts are actually a really great idea.

It is similar to WSDL, BUT GRPC avoids major WSDL failures because they learned with those mistakes.

### SOAP/WSDL

- Inextricably tied to XML (protobuf is pluggable)
- Very heavyweight service definition format: XML/XSD nightmare
- Unnecessarily complex, bloated with unnecessary features (Two-phase commit?!)
- Inflexible and intolerant of forward-compatibility (unlike protobuf)
- Performance, streaming not solved ...
- Machine-readable API contracts are actually a great idea
- Clients were responsible for generating libraries instead of vendors

### What about SWAGGER

- Solves the machine-readable contract problem because we can take a swgger definition and generate API client libraries out of it and server stubs, but none of the other problems with HTTP/JSON (performance, streaming, modeling).
- Swagger definitions are cumbersome and incredibly verbose. Compared to writing gRPC protobuf definitions, they are a gigantic pain.

## What about THRIFT

- Actually a really great idea, very similar project goals
- Never achieved same ubiquity and ease of use. This is really hard. Requires all the major languages implementations to be:
  - Well documented
  - Reliable
  - Highly Performant
  - Easy to Install

## GRPC in production

- RPC Evolution
  - 2 services with HTTP/JSON
  - ~5 services with Thrift
  - 20+ services across 5 regions, all via GRPC
- Tens of millions of RPC calls every day

- Square - replacement for all of their internal RPC. One of the very first adopters and contributors to GRPC.
- CoreOS - Production API for etcd v3 is entirely GRPC
- Google - Production APIs for Google Cloud Services (e.g. PubSub, Speech Rec)
- Netflix, Yik Yak, VSCO, Cockroach, + many more

## Proto

<https://developers.google.com/protocol-buffers/docs/overview>

<https://developers.google.com/protocol-buffers/docs/proto3>

Protocol buffers are a flexible, efficient, automated mechanism for serializing structured data – think XML, but smaller, faster, and simpler. You define how you want your data to be structured once, then you can use special generated source code to easily write and read your structured data to and from a variety of data streams and using a variety of languages. You can even update your data structure without breaking deployed programs that are compiled against the "old" format.

The protobuf definition files is where we define our API as one source and where we define our message format (schema management) and from that we can generate code in multiple different languages. It also gives us a "single source of truth" for our APIs and our services that is really what we have to share with others in order for them to know exactly how to call our services and what to expect back.

A lot better then JSON schemas because even though those are machine-readable they do not have a standard.

A standard.

Once we have that proto file we want to share it, for now, the best way to share it is by having a shared repository where everybody, every service owners, puts their proto files and from there you pull the files that you need to properly generate the client code that we need to call that service. On top of that repo we can write tooling so we have a tool that helps you download the proto files and dependencies. This is also the place where we can enforce guideline and how we write proto files. For example, in Spotify, if we want to submit our proto files to some service we create e pull request and the CI pipeline does linting and additonal checks before it actually can be merged so that is where we can enforce central policies around API design.

Uber tool to handle proto files - proto-tool
Spotify - proto-man

Version on the proto package. Do not do versioning, ofc we are going to have versions but do not try to distribute different proto files with different version in their file name or anything like that or in our client implementation, just rely on the protobuf to being backwards compatible. If we need to do breaking changes we create a new package, proto package. If we are not comfortable with actually publishing that package and setting that version strictly we can call it name+"beta1", a lot of companies follow that pattern.

A language-neutral, platform-neutral, extensible way of serializing structured data for use in communications protocols, data storage, and more.

## Performance gains/losses

## Use cases

## Security

## Logging

## Bi-directional streaming

## Migration

- Change is hard
- 

## Unity and load tests

## Hosting

## Others

Outcomes
Let’s go back to our original criteria:

- Language-neutral
- Fast
- Easy to use

In terms of language support, JSON-backed REST is the clear winner. gRPC’s language support has improved drastically over the last couple of years, however, and it’s arguably sufficient for most use cases.

Our performance comparisons eliminate HTTP/1.1 from all use cases but supporting legacy clients through a front-end API service. Between gRPC and REST over HTTP/2, the performance difference is still significant. Anytime that request performance is a key issue, gRPC seems to be the correct choice.

In terms of ease of use, developers need to write less code to do the same thing in gRPC compared to REST. Debugging is different, but not necessarily any harder. It’s more a problem of developers getting used to a new paradigm.

From our findings, we can see that gRPC is a much better solution for internal service to service communication. It has better performance, improves development speed, and is sufficiently language-neutral. We can conclude that we should default to building gRPC services unless REST is needed to support external clients, or to support a language/platform gRPC isn’t built for yet.

Impacts

- Reduced latency for customers; a better user experience
- Lower processing time for requests; lower costs
- Improved developer efficiency; lower costs for companies and more new features developed

## C-Core

<https://grpc.io/blog/grpc-stacks/>

## Event-driven

- Too complex
- Of course, event-driven architectures have drawbacks as well. They are easy to over-engineer by separating concerns that might be simpler when closely coupled;  can require a significant upfront investment; and often result in additional complexity in infrastructure, service contracts or schemas, polyglot build systems, and dependency graphs. 
Perhaps the most significant drawback and challenge is data and transaction management. Because of their asynchronous nature, event-driven models must carefully handle inconsistent data between services, incompatible versions, watch for duplicate events, and typically do not support ACID transactions, instead supporting eventual consistency which can be more difficult to track or debug.

