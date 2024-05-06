import { BehaviorSubject } from "rxjs";

const deposit = new BehaviorSubject(0);

function add(value: number) {
  deposit.next(deposit.getValue() + value);
}

function reset() {
  deposit.next(0);
}

export default {
  deposit,
  add,
  reset,
};
