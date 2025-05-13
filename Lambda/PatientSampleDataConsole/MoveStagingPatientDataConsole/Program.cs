// See https://aka.ms/new-console-template for more information
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.Lambda.RuntimeSupport;
using MoveStagingPatientDataConsole;

Func< ILambdaContext,Task> handler = new Function().FunctionHandler;
using var handlerWrapper = HandlerWrapper.GetHandlerWrapper(handler);

using var bootstrap = new LambdaBootstrap(handlerWrapper);

await bootstrap.RunAsync();