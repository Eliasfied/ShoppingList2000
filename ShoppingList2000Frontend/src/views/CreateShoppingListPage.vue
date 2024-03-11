<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Create Shopping List</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content>
      <ShoppingListComponent
        :shoppingList="shoppingList"
        @create="create"
      ></ShoppingListComponent>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">

//imports
import { ref, Ref } from "vue";
import { ShoppingList } from "../models/ShoppingList";
import { Product } from "../models/Product";
import { loginStore } from "@/store/loginStore";
import ShoppingListComponent from "@/components/ShoppingListComponent.vue";
import {createShoppingList} from "../services/shoppingListService";
import { shoppingListStore } from "@/store/shoppingListStore";


// loginStore, shoppingListStore
const logStore = loginStore();
const shopStore = shoppingListStore();


// shoppingList
const eligebleUsers = ref(["222"]) as Ref<Array<string>>;
const shoppingListName = ref("");
const creatorUserId = ref("") as Ref<string>;
const products: Ref<Array<Product>> = ref([]);
const lastUpdatedUser = ref("");
const shoppingList = ref(
  new ShoppingList(
    shoppingListName.value,
    creatorUserId.value,
    eligebleUsers.value,
    products.value,
    lastUpdatedUser.value
  )
);

const create = async (shoppingList: ShoppingList) => {
  console.log(shoppingList);
  shoppingList.creatorUserId = logStore.userId || "";
  shoppingList.lastUpdatedUser = logStore.userId || "";
  shoppingList.eligibleUsers = eligebleUsers.value;
  try {
    await createShoppingList(shoppingList);
    shopStore.shoppingLists.push(shoppingList);
  } catch (error) {
    console.error(error);
  }
};
</script>
