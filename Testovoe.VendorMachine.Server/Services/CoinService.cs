using System.Net;
using Testovoe.VendorMachine.Server.Controllers;
using Testovoe.VendorMachine.Server.Data;
using Testovoe.VendorMachine.Server.Models;

namespace Testovoe.VendorMachine.Server.Services;

public class CoinService(AppDbContext appDbContext, ITokenAuthenticator tokenAuthenticator) : ICoinService
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly ITokenAuthenticator _tokenAuthenticator = tokenAuthenticator;

    public IEnumerable<CoinGetResponseDto> ReadAll() =>
        _appDbContext.Coins.Select(s => new CoinGetResponseDto(s.Id, s.Count, s.Value, s.IsBlocked));

    public async Task<CoinPutResponseDto?> Update(CoinPutRequestDto dto, string token, HttpContext context)
    {
        if (!_tokenAuthenticator.Authenticate(token))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return null;
        }

        var attached =
            new Coin { Id = dto.Id, Count = dto.Count, Value = dto.Value, IsBlocked = dto.IsBlocked };

        _appDbContext.Update(attached);
        await _appDbContext.SaveChangesAsync();
        return new CoinPutResponseDto(attached.Id, attached.Count, attached.Value, attached.IsBlocked);
    }

    public async Task<CoinDeleteResponseDto?> Delete(int id, string token, HttpContext context)
    {
        if (!_tokenAuthenticator.Authenticate(token))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return null;
        }

        Coin? toDelete = _appDbContext.Coins.Where(p => p.Id == id).FirstOrDefault();
        if (toDelete == null) return null;
        _appDbContext.Coins.Remove(toDelete);
        await _appDbContext.SaveChangesAsync();
        return new CoinDeleteResponseDto(toDelete.Id, toDelete.Count, toDelete.Value, toDelete.IsBlocked);
    }
}
