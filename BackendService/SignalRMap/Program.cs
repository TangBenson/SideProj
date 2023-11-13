using SignalRMap.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddCors();

var app = builder.Build();

// 加入同源政策支援(CORS)，要在 app.UseRouting之前
app.UseCors(builder =>
{
    builder.AllowAnyHeader().AllowAnyMethod()
    .SetIsOriginAllowed(_ => true).AllowCredentials();
});


app.MapGet("/", () => "Hello World!");
app.MapHub<MotorHub>("/motorHub");

app.Run();
