using System.Text;
using EFCoreService.DbConnect;
using MemDataService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGetDataService, GetDataService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr"));
});

//建立TokenValidationParameters，用來驗證客戶端傳過來的token是否合法
TokenValidationParameters tokenValidationParams = new()
{
    RequireExpirationTime = false,
    // 保哥:一般我們都會驗證 Issuer
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
    
    // 保哥:通常不太需要驗證 Audience
    ValidateAudience = false,
    //ValidAudience = "xxxxxx", // 不驗證就不需要填寫

    //驗證IssuerSigningKey，如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
    ValidateIssuerSigningKey = false,
    //以JwtConfig:Secret為Key,做為Jwt加密
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtSettings").GetValue<string>("SignKey")!)),

    //驗證時效
    ValidateLifetime = true,

    //設定token的過期時間可以以秒來計算,當token的過期時間低於五分鐘時使用。
    ClockSkew = TimeSpan.Zero
};
//註冊tokenValidationParams,後續可以注入使用。
builder.Services.AddSingleton(tokenValidationParams);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
