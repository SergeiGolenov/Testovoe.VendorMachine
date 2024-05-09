using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Testovoe.VendorMachine.Server.Services;

namespace Testovoe.VendorMachine.Server.Controllers;

public record CoinGetResponseDto(int Id, int Count, int Value, bool IsBlocked);
public record CoinPutRequestDto(
    int Id,
    [Range(0, int.MaxValue)] int Count,
    [Range(0, int.MaxValue)] int Value,
    bool IsBlocked);
public record CoinPutResponseDto(int Id, int Count, int Value, bool IsBlocked);
public record CoinDeleteResponseDto(int Id, int Count, int Value, bool IsBlocked);

[Route("api/[controller]")]
[ApiController]
public class CoinController(ICoinService coinService) : ControllerBase
{
    private readonly ICoinService _coinService = coinService;

    [HttpGet]
    public IEnumerable<CoinGetResponseDto> GetAll() => _coinService.ReadAll();

    [HttpPut]
    public async Task<CoinPutResponseDto?> Put(CoinPutRequestDto dto, [FromQuery] string token)
        => await _coinService.Update(dto, token, HttpContext);

    [HttpDelete("{id}")]
    public async Task<CoinDeleteResponseDto?> Delete(int id, [FromQuery] string token)
        => await _coinService.Delete(id, token, HttpContext);
}
