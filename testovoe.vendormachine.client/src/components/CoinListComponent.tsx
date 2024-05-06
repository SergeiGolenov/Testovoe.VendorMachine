import { useEffect, useState } from "react";
import CoinService from "../services/CoinService";
import CoinComponent from "./CoinComponent";

export default function CoinlistComponent() {
  const [coins, setCoins] = useState(CoinService.coins.getValue());

  useEffect(() => {
    const subcription = CoinService.coins.subscribe(setCoins);
    return () => {
      subcription.unsubscribe();
    };
  }, []);

  return (
    <div className="mx-auto mt-4 flex flex-wrap justify-around">
      {(() => coins.map((coin) => <CoinComponent {...coin} />))()}
    </div>
  );
}
