import BillService from "../services/BillService";
import SodaService, { soda } from "../services/SodaService";

export default function SodaComponent(props: soda) {
  function onClick() {
    if (props.count > 0) {
      SodaService.draw(props.id);
      BillService.add(props.price);
    }
  }

  return (
    <div className="mx-2 mt-2 flex flex-col">
      <img
        src={props.image}
        className="h-[200px] w-[100px] select-none self-center"
      ></img>
      <div className="mt-3 w-[140px] bg-neutral-900 text-center font-black uppercase text-green-900">
        <span
          onClick={onClick}
          className="text-pixel mx-1 select-none font-black uppercase text-green-900 hover:brightness-125"
        >
          BUY {props.price}₽
        </span>
      </div>
      <div className="mt-3 w-[140px] bg-neutral-900 text-center">
        <span
          className={
            "text-pixel select-none font-black uppercase" +
            (props.count > 0 ? " text-green-900" : " text-red-900")
          }
        >
          {props.count > 0 ? "IN STOCK " + props.count : "OUT"}
        </span>
      </div>
    </div>
  );
}
