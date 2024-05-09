using Microsoft.AspNetCore.Mvc;
using Testovoe.VendorMachine.Server.Services;

namespace Testovoe.VendorMachine.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController(ITokenAuthenticator tokenAuthenticator) : ControllerBase
{
    private readonly ITokenAuthenticator _tokenAuthenticator = tokenAuthenticator;

    [HttpGet("{token}")]
    public bool Get(string token) => _tokenAuthenticator.Authenticate(token);
}
