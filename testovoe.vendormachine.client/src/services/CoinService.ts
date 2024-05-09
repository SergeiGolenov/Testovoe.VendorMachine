import { BehaviorSubject } from "rxjs";

export class Coin {
  public timesClicked: number = 0;

  constructor(
    public id: number,
    public value: number,
    public count: number,
    public isBlocked: boolean,
  ) {}
}

const coins = new BehaviorSubject([
  new Coin(1, 1, 40, false),
  new Coin(2, 2, 30, false),
  new Coin(3, 5, 20, true),
  new Coin(4, 10, 10, false),
] as Coin[]);

function pullFromServer() {
  fetch("api/Coin").then((response) => {
    response.json().then((result) => {
      coins.next(
        (result as Coin[]).map((coin) => ({ ...coin, timesClicked: 0 })),
      );
    });
  });
}

function draw(id: number) {
  coins.next(
    coins.getValue().map((coin) => {
      if (coin.id == id) {
        return { ...coin, count: coin.count - 1 } as Coin;
      }
      return coin;
    }),
  );
}

function click(id: number) {
  coins.next(
    coins.getValue().map((coin) => {
      if (coin.id == id) {
        return { ...coin, timesClicked: coin.timesClicked + 1 } as Coin;
      }
      return coin;
    }),
  );
}

pullFromServer();

export default { coins, draw, click, pullFromServer };
