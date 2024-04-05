export class Product {
  name: string;
  isPermanent: boolean;
  isChecked: boolean;
  count: number;

  constructor(name: string, isPermanent: boolean, isChecked: boolean, count: number) {
    this.name = name;
    this.isPermanent = isPermanent;
    this.isChecked = isChecked;
    this.count = count;
  }
}
