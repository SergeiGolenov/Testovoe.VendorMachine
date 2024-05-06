import { BehaviorSubject } from "rxjs";

export type coin = {
  id: number;
  value: number;
  count: number;
  isBlocked: boolean;
};

const coins = new BehaviorSubject([
  {
    id: 1,
    value: 1,
    count: 40,
    isBlocked: false,
  },
  {
    id: 2,
    value: 2,
    count: 30,
    isBlocked: false,
  },
  {
    id: 3,
    value: 5,
    count: 20,
    isBlocked: true,
  },
  {
    id: 4,
    value: 10,
    count: 10,
    isBlocked: false,
  },
] as coin[]);

function draw(id: number) {
  coins.next(
    coins.getValue().map((coin) => {
      if (coin.id == id) {
        return { ...coin, count: coin.count - 1 } as coin;
      }
      return coin;
    }),
  );
}

export default { coins, draw };
