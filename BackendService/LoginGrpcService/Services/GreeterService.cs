using EFCoreService.DbConnect;
using EFCoreService.Models;
using Grpc.Core;
using LoginGrpcService;
using LoginGrpcService.Models;

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

    public override async Task<CheckReply> CheckLogin(HelloRequest request, ServerCallContext context)
    {
        //驗證帳密
        var member = _context.Member.Where(x => x.Name == request.Account && x.Basvd == request.Pavvd).FirstOrDefault();
        if (member == null)
        {
            return new CheckReply
            {
                Endmsg = "帳號或密碼錯誤",
                AccessToken = "",
                RefreshToken = ""
            };
        }
        else
        {
            //呼叫api產生jwt
            using (var client = new HttpClient())
            {
                string apiUrl = "http://localhost:5174/api/JWT/GetTk";
                string queryString = $"account={request.Account}&email={request.Account}@xx.com";

                // 使用 UriBuilder 建立完整的 URI，包含 query string
                UriBuilder uriBuilder = new UriBuilder(apiUrl);
                uriBuilder.Query = queryString;

                var response = await client.GetAsync(uriBuilder.Uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var token = System.Text.Json.JsonSerializer.Deserialize<AuthResult>(result);
                    if (token.Result == true)
                    {
                        return new CheckReply
                        {
                            Endmsg = "登入成功",
                            AccessToken = token.AccessToken,
                            RefreshToken = token.RefreshTokeno
                        };
                    }
                    else
                    {
                        return new CheckReply
                        {
                            Endmsg = "產生token失敗",
                            AccessToken = "",
                            RefreshToken = ""
                        };
                    }
                }
                else
                {
                    return new CheckReply
                    {
                        Endmsg = "API呼叫失敗",
                        AccessToken = "",
                        RefreshToken = ""
                    };
                    // return Task.FromResult(new CheckReply
                    // {
                    //     Endmsg = "API呼叫失敗",
                    //     AccessToken = "",
                    //     RefreshToken = ""
                    // });
                }
            }
        }
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
