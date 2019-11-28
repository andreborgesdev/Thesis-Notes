# Workers

## BackgroundWorker

Executes an operation on a separate threat.

C# BackgroundWorker component executes code in a separate dedicated secondary thread. In this article, I will demonstrate how to use the BackgroundWorker component to execute a time consuming process while main thread is still available to the user interface.

If we do not execute our worker in more than 2 services it does not make sense to have it.

## Services

One Service can only have 1 Worker and a worker can be executed in N services. It only makes sense to have a worker when we want to exectue it in more than 2 services. So it only make sense to create workers when we want to implement the same operation for more than 2 services. So every service that we want to create that has a specific logic and task to do don't need to have a worker. Basically we can implement our logic both in the Service or in the Worker depending on that case.

We need to register them in the pipeline. The services implement the worker task in different ways.

- BackgroundService
- TimedBackgroundService : BackgroundService
- QueueBackgroundService : BackgroundService

The difference when using a microservices architecture is that you can implement a single microservice process/container for hosting these background tasks so you can scale it down/up as you need or you can even make sure that it runs a single instance of that microservice process/container.

<https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/background-tasks-with-ihostedservice>

<https://github.com/aspnet/AspNetCore.Docs/tree/master/aspnetcore/fundamentals/host/hosted-services/samples/3.x/BackgroundTasksSample/Services>

Notas no caderno

Transient objects are always different; a new instance is provided to every controller and every service.

Scoped objects are the same within a request, but different across different requests.

Singleton objects are the same for every object and every request.

## Problema 

Como cada lógica do BackgroundService corre no scope de uma única instância e podem haver várias instâncias a correr em paralelo temos que arranjar uma forma que se ligue com elas todas para previnir race conditions. Instância ou MS porque 2 MS podem usar o mesmo recurso.