import { BehaviorSubject } from "rxjs";
import coke from "../assets/coke.png";
import pepsi from "../assets/pepsi.png";
import sprite from "../assets/sprite.png";
import fanta from "../assets/fanta.png";

export type soda = {
  id: number;
  image: string;
  price: number;
  count: number;
};

const sodas = new BehaviorSubject([
  {
    id: 1,
    image: coke,
    price: 49,
    count: 6,
  },
  {
    id: 2,
    image: pepsi,
    price: 59,
    count: 8,
  },
  {
    id: 3,
    image: sprite,
    price: 58,
    count: 12,
  },
  {
    id: 4,
    image: fanta,
    price: 39,
    count: 4,
  },
] as soda[]);

function draw(id: number) {
  sodas.next(
    sodas.getValue().map((sodas) => {
      if (sodas.id == id) {
        return { ...sodas, count: sodas.count - 1 } as soda;
      }
      return sodas;
    }),
  );
}

export default { sodas, draw };
