# Workers

## BackgroundWorker

Executes an operation on a separate threat.

If we do not execute our worker in more than 2 services it does not make sense to have it.

C# BackgroundWorker component executes code in a separate dedicated secondary thread. In this article, I will demonstrate how to use the BackgroundWorker component to execute a time consuming process while main thread is still available to the user interface.

## Services

We need to register them in the pipeline. The services implement the worker task in different ways.

- BackgroundService
- TimedBackgroundService : BackgroundService
- QueueBackgroundService : BackgroundService

The difference when using a microservices architecture is that you can implement a single microservice process/container for hosting these background tasks so you can scale it down/up as you need or you can even make sure that it runs a single instance of that microservice process/container.

<https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/background-tasks-with-ihostedservice>

<https://github.com/aspnet/AspNetCore.Docs/tree/master/aspnetcore/fundamentals/host/hosted-services/samples/3.x/BackgroundTasksSample/Services>

Notas no caderno
