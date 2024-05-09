namespace Testovoe.VendorMachine.Server.Services;

public interface ITokenAuthenticator
{
    bool Authenticate(string token);
}
