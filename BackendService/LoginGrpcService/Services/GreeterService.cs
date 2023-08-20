using Grpc.Core;
using LoginGrpcService;

namespace LoginGrpcService.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Endmsg = "Hello " + request.Acount,
            AccessToken = "Hello " + request.Pavvd,
            RefreshToken = "Hello " + request.Pavvd
        });
    }
}
