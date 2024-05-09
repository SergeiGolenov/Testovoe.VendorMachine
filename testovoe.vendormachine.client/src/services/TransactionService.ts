import BillService from "./BillService";
import CoinService from "./CoinService";
import DepositService from "./DepositService";
import SodaService from "./SodaService";

export type TransactionIdCountPairDto = {
  id: number;
  count: number;
};

export type TransactionPostDto = {
  sodas: TransactionIdCountPairDto[];
  coins: TransactionIdCountPairDto[];
};

function commit() {
  fetch("api/Transaction", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      sodas: SodaService.sodas
        .getValue()
        .filter((soda) => soda.timesClicked > 0)
        .map(
          (soda) =>
            ({
              id: soda.id,
              count: soda.timesClicked,
            }) as TransactionIdCountPairDto,
        ),
      coins: CoinService.coins
        .getValue()
        .filter((coin) => coin.timesClicked > 0)
        .map(
          (coin) =>
            ({
              id: coin.id,
              count: coin.timesClicked,
            }) as TransactionIdCountPairDto,
        ),
    } as TransactionPostDto),
  }).then(() => {
    BillService.reset();
    DepositService.reset();
    SodaService.pullFromServer();
    CoinService.pullFromServer();
  });
}

export default { commit };
