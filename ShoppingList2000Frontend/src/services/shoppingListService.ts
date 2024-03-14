import https from "./https";
import { ShoppingList } from "../models/ShoppingList";

export const createShoppingList = async (shoppingList: ShoppingList) => {
  return await https.post("/api/shoppingList/create", shoppingList);
};

export const updateShoppingList = async (
  shoppingList: ShoppingList,
  userId: string
) => {
  return await https.post("/api/shoppingList/update?userId=" + userId, {
    ...shoppingList,
  });
};

export const getShoppingLists = async (userId: string | null) => {
  return await https.get("/api/shoppingList/getAll?userId=" + userId);
}


export const deleteShoppingList = async (shoppingListId: string | null | undefined, userId: string | null) => {
  return await https.post("/api/shoppingList/delete?shoppingListId=" + shoppingListId + "&userId=" + userId);
}
