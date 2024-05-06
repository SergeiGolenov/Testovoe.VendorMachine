import { useEffect, useState } from "react";
import SodaService from "../services/SodaService";
import SodaComponent from "./SodaComponent";

export default function SodaListComponent() {
  const [sodas, setSodas] = useState(SodaService.sodas.getValue());

  useEffect(() => {
    const subscription = SodaService.sodas.subscribe(setSodas);
    return () => {
      subscription.unsubscribe();
    };
  }, []);

  return (
    <div className="mt-10 flex w-full flex-wrap justify-around">
      {(() => sodas.map((soda) => <SodaComponent {...soda} />))()}
    </div>
  );
}
