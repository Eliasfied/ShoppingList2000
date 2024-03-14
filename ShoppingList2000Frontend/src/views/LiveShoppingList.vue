<template>
  <ion-page class="page-background">
    <ion-header>
      <ion-toolbar>
        <ion-title slot="start" color="primary">{{
          shoppingList?.shoppingListName
        }}</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content>
        <ion-list class="list" v-if="shoppingList">
          <ion-item v-for="(item, index) in shoppingList.products" :key="index">
            <ion-checkbox
              label-placement="end"
              justify="start"
              @ion-change="update"
              v-model="item.isChecked"
            >
              {{ item.name }}</ion-checkbox
            >
            <ion-button
              fill="clear"
              color="danger"
              slot="end"
              @click="removeItem(index)"
            >
              <ion-icon :icon="trashOutline"></ion-icon>
            </ion-button>
          </ion-item>
        </ion-list>
        <ion-item>
          <ion-input v-model="newItem" placeholder="Add Item"></ion-input>
          <ion-button @click="addItem">
            <ion-icon :icon="addOutline"></ion-icon>
          </ion-button>
        </ion-item>
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
import { trashOutline, addOutline } from "ionicons/icons";

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

const newItem = ref("");
const addItem = async () => {
  if (newItem.value && shoppingList.value) {
    shoppingList.value.products.push({
      name: newItem.value,
      isPermanent: false,
      isChecked: false,
    });
    newItem.value = "";
    await update();
  }
};
const removeItem = async (index: number) => {
  if (shoppingList.value) {
    shoppingList.value.products.splice(index, 1);
    await update();
  }
};

const update = async () => {
  try {
    if (shoppingList.value) {
      shoppingList.value.lastUpdatedUser = logStore.userId || "";
      await updateShoppingList(shoppingList.value, logStore.userId || "");
    }
  } catch (error) {
    console.error(error);
  }
};
</script>

<style scoped>
ion-checkbox {
  --size: 32px;
  --checkbox-background-checked: var(--ion-color-primary);
}

ion-checkbox::part(container) {
  border-radius: 20px;
  border: 2px solid var(--ion-color-primary);
}

ion-checkbox::part(label) {
  font-weight: bold;
  color: black;
}


ion-content {
  --background: linear-gradient(rgba(255, 255, 255, 0.7), rgba(255, 255, 255, 0.2)), url("../assets/live-background.png") no-repeat center center / cover;
}


ion-list {
  --ion-background-color: transparent !important;
}

ion-item {
  --ion-background-color: transparent !important;
}

ion-input {
  --ion-background-color: transparent !important;

}


.list {
    margin-top: 10%;
}
</style>
