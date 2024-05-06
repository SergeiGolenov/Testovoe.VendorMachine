import { useEffect, useState } from "react";
import DepositService from "../services/DepositService";
import BillService from "../services/BillService";

export default function InfoBarComponent() {
  const [deposit, setDeposit] = useState(DepositService.deposit.getValue());
  const [bill, setBill] = useState(BillService.bill.getValue());

  useEffect(() => {
    const depositSubscription = DepositService.deposit.subscribe(setDeposit);
    const billSubscription = BillService.bill.subscribe(setBill);
    return () => {
      depositSubscription.unsubscribe();
      billSubscription.unsubscribe();
    };
  }, []);

  return (
    <div className="mt-10 flex w-full flex-wrap justify-around bg-neutral-900">
      <span className="text-pixel mx-1 select-none text-lg font-black text-yellow-800">
        DEPOSIT {deposit}₽
      </span>
      <span className="text-pixel mx-1 select-none text-lg font-black text-yellow-800">
        BILL {bill}₽
      </span>
    </div>
  );
}
