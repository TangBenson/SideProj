using EFCoreService.DbConnect;
using EFCoreService.Models;
using Grpc.Core;
using LoginGrpcService;

namespace LoginGrpcService.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly AppDbContext _context;
    public GreeterService(ILogger<GreeterService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public override Task<CheckReply> CheckLogin(HelloRequest request, ServerCallContext context)
    {
        //驗證帳密


        //產生jwt


        return Task.FromResult(new CheckReply
        {
            Endmsg = "GG, " + request.Account,
            AccessToken = "RR, " + request.Pavvd,
            RefreshToken = "嗨, " + request.Pavvd
        });
    }
    public override Task<CreateReply> CreateAccount(HelloRequest request, ServerCallContext context)
    {
        string ressult;
        if (_context.Member.Where(x => x.Name == request.Account).Any())
        {
            ressult = "帳號已存在";
        }
        else
        {
            _context.Member.Add(new MemberData
            {
                ID = "",
                Name = request.Account,
                Basvd = request.Pavvd,
                Email = "",
                Phone = ""
            });
            _context.SaveChanges();
            ressult = "帳號建立成功";
        }
        return Task.FromResult(new CreateReply
        {
            Result = ressult
        });
    }
}
