using Microsoft.AspNetCore.Mvc;
using OauthService.Service;

namespace OauthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet(Name = "signin")]
    public async Task<IActionResult> SigninOauthAsync([FromQuery] string code)
    {
        Console.WriteLine(code);
        await Task.Delay(1000);
        await AuthService.GetIdTokenAsync(code);
        return Ok();
    }
}
