import httpswithToken from "./httpsWithToken";
import { ShoppingList } from "../models/ShoppingList";

export const createShoppingList = async (shoppingList: ShoppingList) => {
  return await httpswithToken.post("/api/shoppingList/create", shoppingList);
};

export const updateShoppingList = async (
  shoppingList: ShoppingList,
  userId: string
) => {
  return await httpswithToken.post("/api/shoppingList/update?userId=" + userId, {
    ...shoppingList,
  });
};

export const getShoppingLists = async (userId: string | null) => {
  return await httpswithToken.get("/api/shoppingList/getAll?userId=" + userId);
}


export const deleteShoppingList = async (shoppingListId: string | null | undefined, userId: string | null) => {
  return await httpswithToken.post("/api/shoppingList/delete?shoppingListId=" + shoppingListId + "&userId=" + userId);
}

export const shareShoppinglist = async (senderId: string, receiverId: string, shoppingListId: string) => {
  return await httpswithToken.post("/api/ShoppingList/share", {
    senderId,
    receiverId,
    shoppingListId,
  });
};
