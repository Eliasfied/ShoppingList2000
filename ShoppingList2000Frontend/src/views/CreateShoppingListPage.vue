<template>
  <ion-page>
    <ion-content class="ion-padding">
      <div class="grid-style-create">
        <ion-title color="primary" class="login-title">Create List</ion-title>
        <div>
          <ion-input
            v-model="shoppingList.shoppingListName"
            type="text"
            label="New List"
            label-placement="floating"
            fill="outline"
            placeholder="Enter Name"
          ></ion-input>
        </div>
        <div>
          <ion-button expand="full" @click="create">Create</ion-button>
        </div>
        <div class="pommes-div"></div>
      </div>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
//imports
import { ref, Ref } from "vue";
import { ShoppingList } from "../models/ShoppingList";
import { Product } from "../models/Product";
import { loginStore } from "@/store/loginStore";
import { createShoppingList } from "../services/shoppingListService";
import { shoppingListStore } from "@/store/shoppingListStore";
import { IonPage } from "@ionic/vue";
import { useRouter } from "vue-router";


//router

const router = useRouter();
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

const create = async () => {
  shoppingList.value.creatorUserId = logStore.userId || "";
  shoppingList.value.lastUpdatedUser = logStore.userId || "";
  shoppingList.value.eligibleUsers = eligebleUsers.value;
  try {
    const response = await createShoppingList(shoppingList.value);
    shoppingList.value.shoppingListId = response.data.shoppingListId;
    shopStore.shoppingLists.push(shoppingList.value);
    goToShoppingList(response.data.shoppingListId);
  } catch (error) {
    console.error(error);
  }
};

const goToShoppingList = (shoppinglistId: any) => {
  if (shoppinglistId) {
    router.push("/liveShoppingList/" + shoppinglistId);
  } else {
    console.log("shoppingListId is undefined");
  }
};
</script>

<style scoped>
ion-content {
  --background: linear-gradient(
      rgba(255, 255, 255, 0.7),
      rgba(255, 255, 255, 0.2)
    ),
    url("../assets/create-background.png") no-repeat center center / cover;
}

.grid-style-create {
  display: grid;
  height: 100%;
  grid-template-rows: [row1-start] 25% [row1-end] 10% [row2-start] 10% [row2-end] 55% [row3-start];
}

ion-input {
  width: 100%;
  height: 20%;
}

.login-title {
  font-size: 3em;
  text-align: center;
  margin-bottom: 60px;
  margin-top: 60px;
}
</style>
