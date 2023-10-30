using System.Text;
using JWTService.Models;
using JWTService.Services;
using JWTService.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

#region 測試一些取值,與jwt無關
// builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JwtSettings"));//將"class JwtConfig"中的"Secret"賦值為"appsettings.json"中的"JwtConfig"
#endregion

#region JwtAuthService2用的,這個範例service是手刻整套jwt流程,包含編碼+雜湊,又多加aes加密
/*
這邊要好好解釋一下,之前JwtAuthService2建構子只帶入config和httpcontext時,AddScoped設定不用帶入任何簽章,因為
config和httpcontext會自動注入給所有程式,這段是.net自動加的。而當JwtAuthService2建構子要多帶入一個簽章時,我就必須
在AddScoped設定,但為何要連config和httpcontext也帶入,不是說自動嗎? 其實自動帶入是指programe以外的程式,因為.net
自動加config和httpcontext是隱藏在programe裡的,所以若在programe就要new物件時,還是需要帶入config和httpcontext。
以上是我自己理解的,不會有錯的
*/
// // builder.Services.AddScoped<JwtAuthService2>();
// builder.Services.AddScoped<JwtAuthService2>(serviceProvider =>
// {
//     var configuration = serviceProvider.GetRequiredService<IConfiguration>();
//     var httpcontext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
//     return new JwtAuthService2(configuration, httpcontext, "test");
// });
#endregion

#region JwtAuthService用的(https://medium.com/selectprogram/asp-net-core%E4%BD%BF%E7%94%A8jwt%E9%A9%97%E8%AD%89-1b0609e6e8e3)
builder.Services.AddSingleton<IJwtAuthService, JwtAuthService>();
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JwtSettings"));//將"class JwtConfig"中的"Secret"賦值為"appsettings.json"中的"JwtConfig"
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
//AddAuthentication註冊驗證的服務，並設定JWT的驗證配置
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt =>
    {
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = tokenValidationParams;
    });
#endregion

#region 另一個寫法(https://hackmd.io/@JYU/rkPen9VKu)
// builder.Services.AddSingleton<IJwtAuthService, JwtAuthService>();
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //這裡作用為告訴app驗證方式為JWT Token驗證
//                 .AddJwtBearer(options =>
//                 {
//                     // You need to import package as follow
//                     // using Microsoft.IdentityModel.Tokens;
//                     options.TokenValidationParameters = new TokenValidationParameters
//                     {
//                         // 配置驗證發行者
//                         ValidateIssuer = true, // 是否要啟用驗證發行者
//                         ValidIssuer = builder.Configuration.GetSection("JwtSettings").GetValue<string>("Issuer"),

//                         // 配置驗證接收方
//                         ValidateAudience = false, // 是否要啟用驗證接收者
//                         // ValidAudience = "" // 如果不需要驗證接收者可以註解

//                         // 配置驗證Token有效期間
//                         ValidateLifetime = true, // 是否要啟用驗證有效時間

//                         // 配置驗證金鑰
//                         ValidateIssuerSigningKey = false, // 是否要啟用驗證金鑰,一般不需要去驗證,因為通常Token內只會有簽章

//                         // 配置簽章驗證用金鑰
//                         // 這裡配置是用來解Http Request內Token加密
//                         // 如果Secret Key跟當初建立Token所使用的Secret Key不一樣的話會導致驗證失敗
//                         IssuerSigningKey = new SymmetricSecurityKey(
//                             Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings").GetValue<string>("SignKey"))
//                         )
//                     };
//                 });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello from .Net 7 API!");

// app.UseHttpsRedirection(); //會把http導向https,但我家裡電腦好像有問題

// UseAuthorization授權之前加入UseAuthentication識別身分。這樣才能在授權之前，先驗證是否為有效的身分
// 識別身分,需加在UseAuthorization之前。要包在app.UseRouting()跟app.UseEndpoints中間..(為何我沒有?)。
// 這個會跟Action上方Tag[Authorize]與[AllowAnonymous]有關
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
