import { BehaviorSubject } from "rxjs";

const bill = new BehaviorSubject(0);

function add(value: number) {
  bill.next(bill.getValue() + value);
}

function reset() {
  bill.next(0);
}

export default {
  bill,
  add,
  reset,
};
