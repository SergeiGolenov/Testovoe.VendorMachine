import CoinService, { Coin } from "../services/CoinService";
import DepositService from "../services/DepositService";

export default function CoinComponent(props: Coin) {
  function onClick() {
    if (props.count > 0 && !props.isBlocked) {
      DepositService.add(props.value);
      CoinService.draw(props.id);
      CoinService.click(props.id);
    }
  }

  return (
    <div className="flex flex-col items-center">
      <div
        onClick={onClick}
        className="mx-1 flex size-16 items-center justify-center rounded-full border-2 border-zinc-400 bg-zinc-500 text-center hover:brightness-125"
      >
        <span className="select-none text-3xl text-zinc-400">
          {props.value}₽
        </span>
      </div>
      <span
        className={
          "mt-2 select-none font-bold" +
          (props.count > 0 && !props.isBlocked
            ? " text-neutral-300"
            : " text-red-800")
        }
      >
        {props.isBlocked ? "BAN" : props.count}
      </span>
    </div>
  );
}
