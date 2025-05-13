using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.Lambda.RuntimeSupport;
using PatientStagingUploaderConsole;


Func<S3Event, ILambdaContext, Task> handler = new Function().FunctionHandler;
using var handlerWrapper = HandlerWrapper.GetHandlerWrapper(handler, new Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer());

using var bootstrap = new LambdaBootstrap(handlerWrapper);

await bootstrap.RunAsync();








