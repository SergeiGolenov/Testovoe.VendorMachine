using Testovoe.VendorMachine.Server.Controllers;

namespace Testovoe.VendorMachine.Server.Services;

public interface ITransactionService
{
    Task<TransactionResult> Create(TransactionPostRequestDto dto);
}
