using Testovoe.VendorMachine.Server.Controllers;

namespace Testovoe.VendorMachine.Server.Services;

public interface ICoinService
{
    IEnumerable<CoinGetResponseDto> ReadAll();
    Task<CoinPutResponseDto?> Update(CoinPutRequestDto dto, string token, HttpContext context);
    Task<CoinDeleteResponseDto?> Delete(int id, string token, HttpContext context);
}
