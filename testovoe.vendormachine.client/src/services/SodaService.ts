import { BehaviorSubject } from "rxjs";
import coke from "../assets/coke.png";
import pepsi from "../assets/pepsi.png";
import sprite from "../assets/sprite.png";
import fanta from "../assets/fanta.png";

export class Soda {
  public timesClicked = 0;

  constructor(
    public id: number,
    public count: number,
    public price: number,
    public image: string,
  ) {}
}

const sodas = new BehaviorSubject([
  new Soda(1, 5, 49, coke),
  new Soda(2, 3, 59, pepsi),
  new Soda(3, 6, 39, sprite),
  new Soda(4, 2, 29, fanta),
] as Soda[]);

function pullFromServer() {
  fetch("api/Soda").then((response) => {
    response.json().then((result) => {
      sodas.next(
        (result as Soda[]).map((soda) => ({
          ...soda,
          image: "data:image/png;base64," + soda.image,
          timesClicked: 0,
        })),
      );
    });
  });
}

pullFromServer();

function draw(id: number) {
  sodas.next(
    sodas.getValue().map((sodas) => {
      if (sodas.id == id) {
        return { ...sodas, count: sodas.count - 1 } as Soda;
      }
      return sodas;
    }),
  );
}

function click(id: number) {
  sodas.next(
    sodas.getValue().map((soda) => {
      if (soda.id == id) {
        return { ...soda, timesClicked: soda.timesClicked + 1 } as Soda;
      }
      return soda;
    }),
  );
}

export default { sodas, draw, click, pullFromServer };
