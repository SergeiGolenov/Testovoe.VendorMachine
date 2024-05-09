using Testovoe.VendorMachine.Server.Controllers;

namespace Testovoe.VendorMachine.Server.Services;

public interface ISodaService
{
    IEnumerable<SodaGetResponseDto> ReadAll();
    Task<SodaPutResponseDto?> Update(SodaPutRequestDto dto, string token, HttpContext context);
    Task<SodaDeleteResponseDto?> Delete(int id, string token, HttpContext context);
}
