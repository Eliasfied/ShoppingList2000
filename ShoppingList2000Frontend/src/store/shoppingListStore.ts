import { defineStore } from "pinia";
import { ShoppingList } from "@/models/ShoppingList";
import { Product } from "@/models/Product";

export const shoppingListStore = defineStore({
  id: "shoppingList",
  state: () => ({
    shoppingLists: [] as Array<ShoppingList | undefined>,
  }),
  actions: {
    getShoppingList(shoppingListId: string) {
      return this.shoppingLists.find(
        (list: ShoppingList | undefined) =>
          list != undefined && list.shoppingListId === shoppingListId
      );
    },
    addShoppingList(shoppingList: ShoppingList) {
      this.shoppingLists.push(shoppingList);
    },

    addToList(item: Product, index: number) {
      const shoppingList = this.shoppingLists[index];
      if (shoppingList) {
        shoppingList.products.push(item);
      } else {
        console.error(`Shopping list at index ${index} is undefined`);
      }
    },
    removeFromList(index: number, index2: number) {
      const shoppinglist = this.shoppingLists[index];
      if (shoppinglist) {
        shoppinglist.products.splice(index2, 1);
      } else {
        console.error(`Shopping list at index ${index} is undefined`);
      }
    },
    updateShoppingList(shoppingList: ShoppingList) {
      const index = this.shoppingLists.findIndex(
        (list: ShoppingList | undefined) =>
          list != undefined &&
          list.shoppingListId === shoppingList.shoppingListId
      );
      this.shoppingLists[index] = shoppingList;
    },
  },
});
