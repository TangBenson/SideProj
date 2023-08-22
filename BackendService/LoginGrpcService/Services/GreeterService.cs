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

    public override Task<HelloReply> CheckLogin(HelloRequest request, ServerCallContext context)
    {
        //呼叫jwt驗證
        

        return Task.FromResult(new HelloReply
        {
            Endmsg = "GG, " + request.Acount,
            AccessToken = "RR, " + request.Pavvd,
            RefreshToken = "嗨, " + request.Pavvd
        });
    }
}
