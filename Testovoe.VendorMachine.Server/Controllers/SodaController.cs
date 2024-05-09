using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Testovoe.VendorMachine.Server.Services;

namespace Testovoe.VendorMachine.Server.Controllers;

public record SodaGetResponseDto(int Id, int Count, int Price, string Image);
public record SodaPutRequestDto(
    int Id,
    [Range(0, int.MaxValue)] int Count,
    [Range(0, int.MaxValue)] int Price,
    IFormFile Image);
public record SodaPutResponseDto(int Id, int Count, int Price);
public record SodaDeleteResponseDto(int Id, int Count, int Price);

[Route("api/[controller]")]
[ApiController]
public class SodaController(ISodaService sodaService) : ControllerBase
{
    private readonly ISodaService _sodaService = sodaService;

    [HttpGet]
    public IEnumerable<SodaGetResponseDto> GetAll() => _sodaService.ReadAll();

    [HttpPut]
    public async Task<SodaPutResponseDto?> Put([FromForm] SodaPutRequestDto dto, [FromQuery] string token)
        => await _sodaService.Update(dto, token, HttpContext);

    [HttpDelete("{id}")]
    public async Task<SodaDeleteResponseDto?> Delete(int id, [FromQuery] string token)
        => await _sodaService.Delete(id, token, HttpContext);
}
