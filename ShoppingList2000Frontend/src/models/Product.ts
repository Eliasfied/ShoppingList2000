export class Product {
  name: string;
  isPermanent: boolean;
  isChecked: boolean;

  constructor(name: string, isPermanent: boolean, isChecked: boolean) {
    this.name = name;
    this.isPermanent = isPermanent;
    this.isChecked = isChecked;
  }
}
