import { Product } from './Product';

export class ShoppingList {
  shoppingListId: string | undefined | null;
  shoppingListName: string;
  products: Product[];
  creatorUserId: string;
  eligibleUsers: string[];
  lastUpdatedUser: string | undefined | null;

  constructor(shoppingListName: string, creatorUserId: string, eligibleUsers: string[], products: Product[] = [], lastUpdatedUser: string) {
    this.shoppingListId = null;
    this.shoppingListName = shoppingListName;
    this.creatorUserId = creatorUserId;
    this.eligibleUsers = eligibleUsers;
    this.products = products.map(product => new Product(product.name, product.isPermanent, product.isChecked));
    this.lastUpdatedUser = lastUpdatedUser;
  }
}