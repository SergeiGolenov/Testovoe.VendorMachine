using Microsoft.AspNetCore.Mvc;
using Testovoe.VendorMachine.Server.Services;

namespace Testovoe.VendorMachine.Server.Controllers;

public record TransactionIdCountPairDto(int Id, int Count);

public record TransactionPostRequestDto(
    IEnumerable<TransactionIdCountPairDto> Sodas,
    IEnumerable<TransactionIdCountPairDto> Coins);

[Route("api/[controller]")]
[ApiController]
public class TransactionController(
    ITransactionService transactionService) : ControllerBase
{
    private readonly ITransactionService _transactionService = transactionService;

    [HttpPost]
    public async Task<TransactionResult> Post(TransactionPostRequestDto dto)
        => await _transactionService.Create(dto);
}
