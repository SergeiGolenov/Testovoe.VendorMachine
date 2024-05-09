namespace Testovoe.VendorMachine.Server.Services;

public class TokenAuthenticator(IConfiguration configuration) : ITokenAuthenticator
{
    private readonly IConfiguration _configuration = configuration;

    public bool Authenticate(string token) => token == _configuration["AdminToken"];
}
