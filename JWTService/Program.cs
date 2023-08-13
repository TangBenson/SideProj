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
builder.Services.AddScoped<JwtAuthService2>();

builder.Services.AddScoped<IJwtAuthService, JwtAuthService>();
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // You need to import package as follow
                    // using Microsoft.IdentityModel.Tokens;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 配置驗證發行者
                        ValidateIssuer = true, // 是否要啟用驗證發行者
                        ValidIssuer = builder.Configuration.GetSection("JwtSettings").GetValue<string>("Issuer"),

                        // 配置驗證接收方
                        ValidateAudience = false, // 是否要啟用驗證接收者
                        // ValidAudience = "" // 如果不需要驗證接收者可以註解

                        // 配置驗證Token有效期間
                        ValidateLifetime = true, // 是否要啟用驗證有效時間

                        // 配置驗證金鑰
                        ValidateIssuerSigningKey = false, // 是否要啟用驗證金鑰，一般不需要去驗證，因為通常Token內只會有簽章

                        // 配置簽章驗證用金鑰
                        // 這裡配置是用來解Http Request內Token加密
                        // 如果Secret Key跟當初建立Token所使用的Secret Key不一樣的話會導致驗證失敗
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                builder.Configuration.GetSection("JwtSettings").GetValue<string>("SignKey")
                            )
                        )
                    };
                });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello from .Net 7 API!");

// app.UseHttpsRedirection(); //會把http導向https，但我家裡電腦好像有問題

// 識別身分，需加在UseAuthorization之前。要包在app.UseRouting()跟app.UseEndpoints中間..(為何我沒有?)。
// 這個會跟Action上方Tag[Authorize]與[AllowAnonymous]有關
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
