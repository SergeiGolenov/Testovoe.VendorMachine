using System.Net;
using Testovoe.VendorMachine.Server.Controllers;
using Testovoe.VendorMachine.Server.Data;
using Testovoe.VendorMachine.Server.Models;

namespace Testovoe.VendorMachine.Server.Services;

public class SodaService(AppDbContext appDbContext, ITokenAuthenticator tokenAuthenticator) : ISodaService
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly ITokenAuthenticator _tokenAuthenticator = tokenAuthenticator;

    public IEnumerable<SodaGetResponseDto> ReadAll()
        => _appDbContext.Sodas.Select(s =>
            new SodaGetResponseDto(s.Id, s.Count, s.Price, Convert.ToBase64String(s.Image)));

    public async Task<SodaPutResponseDto?> Update(SodaPutRequestDto dto, string token, HttpContext context)
    {
        if (!_tokenAuthenticator.Authenticate(token))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return null;
        }

        using Stream imageStream = dto.Image.OpenReadStream();
        var imageBuffer = new byte[imageStream.Length];
        await imageStream.ReadAsync(imageBuffer);

        var attached = new Soda { Id = dto.Id, Count = dto.Count, Price = dto.Price, Image = imageBuffer };
        _appDbContext.Update(attached);

        await _appDbContext.SaveChangesAsync();
        return new SodaPutResponseDto(attached.Id, attached.Count, attached.Price);
    }

    public async Task<SodaDeleteResponseDto?> Delete(int id, string token, HttpContext context)
    {
        if (!_tokenAuthenticator.Authenticate(token))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return null;
        }

        Soda? toDelete = _appDbContext.Sodas.Where(p => p.Id == id).FirstOrDefault();
        if (toDelete == null) return null;
        _appDbContext.Sodas.Remove(toDelete);
        await _appDbContext.SaveChangesAsync();
        return new SodaDeleteResponseDto(toDelete.Id, toDelete.Count, toDelete.Price);
    }
}
