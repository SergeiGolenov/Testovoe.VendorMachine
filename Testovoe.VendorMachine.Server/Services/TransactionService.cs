using Microsoft.EntityFrameworkCore;
using Testovoe.VendorMachine.Server.Controllers;
using Testovoe.VendorMachine.Server.Data;
using Testovoe.VendorMachine.Server.Models;

namespace Testovoe.VendorMachine.Server.Services;

public abstract record TransactionResult(string Message)
{
    public record Success()
        : TransactionResult("Success");
    public record PriceHigherThanPay()
        : TransactionResult("The price is higher than the pay");
    public record InvalidSoda()
        : TransactionResult("One or more of the requested sodas is invalid");
    public record InvalidCoin()
        : TransactionResult("One or more of the requested coins is invalid");
    public record SelectedBlockedCoin()
        : TransactionResult("Selected blocked coin");
    public record NotEnoughSoda()
        : TransactionResult("Not enough soda");
    public record NotEnoughCoins()
        : TransactionResult("Not enough coins");
}

public class TransactionService(AppDbContext appDbContext) : ITransactionService
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<TransactionResult> Create(TransactionPostRequestDto dto)
    {
        List<Soda> storedSodas = await _appDbContext.Sodas.ToListAsync();
        List<Coin> storedCoins = await _appDbContext.Coins.ToListAsync();

        if (dto.Sodas.ExceptBy(storedSodas.Select(s => s.Id), s => s.Id).Any())
            return new TransactionResult.InvalidSoda();

        if (dto.Coins.ExceptBy(storedCoins.Select(s => s.Id), s => s.Id).Any())
            return new TransactionResult.InvalidCoin();

        if (storedCoins.IntersectBy(dto.Coins.Select(s => s.Id), s => s.Id).Where(p => p.IsBlocked).Any())
            return new TransactionResult.SelectedBlockedCoin();

        foreach (var sodaToBuy in dto.Sodas)
            storedSodas.Where(p => p.Id == sodaToBuy.Id).Single().Count -= sodaToBuy.Count;
        if (storedSodas.Where(p => p.Count < 0).Any()) return new TransactionResult.NotEnoughSoda();

        foreach (var coinToPay in dto.Coins)
            storedCoins.Where(p => p.Id == coinToPay.Id).Single().Count -= coinToPay.Count;
        if (storedCoins.Where(p => p.Count < 0).Any()) return new TransactionResult.NotEnoughCoins();

        int paySum = dto.Coins.Aggregate(0,
            (sum, next) => sum + next.Count * storedCoins.Where(p => p.Id == next.Id).Single().Value);

        int priceSum = dto.Sodas.Aggregate(0,
            (sum, next) => sum + next.Count * storedSodas.Where(p => p.Id == next.Id).Single().Price);

        if (paySum < priceSum) return new TransactionResult.PriceHigherThanPay();

        int change = paySum - priceSum;

        foreach (Coin orderedCoin in storedCoins.OrderByDescending(s => s.Value))
        {
            if (orderedCoin.IsBlocked) continue;
            orderedCoin.Count += (int)Math.Floor((decimal)(change / orderedCoin.Value));
            change %= orderedCoin.Value;
        }

        _appDbContext.SaveChanges();
        return new TransactionResult.Success();
    }
}
