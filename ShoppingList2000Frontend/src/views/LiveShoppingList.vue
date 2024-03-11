<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Shopping List</ion-title>
      </ion-toolbar>
    </ion-header>

    <ion-content>
      <ion-list v-if="shoppingList">
        <ion-item v-for="(item, index) in shoppingList.products" :key="index">
          <ion-checkbox
            justify="space-between"
            @ion-change="update"
            v-model="item.isChecked"
          >
            {{ item.name }}</ion-checkbox
          >
        </ion-item>
      </ion-list>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
//imports
import { useRoute } from "vue-router";
import { ref, onMounted } from "vue";
import { shoppingListStore } from "@/store/shoppingListStore";
import { ShoppingList } from "@/models/ShoppingList";
import { IonCheckbox } from "@ionic/vue";
import { updateShoppingList } from "@/services/shoppingListService";
import { loginStore } from "@/store/loginStore";
import { SignalRService } from "@/services/signalRService";

//store, route, params
const route = useRoute();
const params = route.params;
const shopStore = shoppingListStore();

const logStore = loginStore();

//SignalR
const signalRService = new SignalRService();

//shoppingList
const shoppingList = ref<ShoppingList | undefined>();

onMounted(async () => {
  shoppingList.value = shopStore.getShoppingList(params.id as string);
  await signalRService.startConnection();
  signalRService.addUpdateShoppingListListener(
    (updatedShoppingList: ShoppingList) => {
      console.log("bin drin 1");
      if (
        logStore.userId &&
        (logStore.userId == updatedShoppingList.creatorUserId ||
          updatedShoppingList.eligibleUsers.includes(logStore.userId))
      ) {
        console.log("bin drin 2");

        if (updatedShoppingList.lastUpdatedUser !== logStore.userId) {
          // Update the shopping list
          console.log("bin drin 3");
          shoppingList.value = updatedShoppingList;
        }
      }
    }
  );
});

const update = async () => {
  try {
    if (shoppingList.value) {
      shoppingList.value.lastUpdatedUser = logStore.userId || "";
      await updateShoppingList(shoppingList.value, logStore.userId || "");
      //await signalRService.sendUpdateShoppingList(shoppingList.value);
    }
  } catch (error) {
    console.error(error);
  }
};
</script>
