<template>
  <ion-page class="page-background">
    <HeaderComponent :title="shoppingList?.shoppingListName" />
    <ion-content>
      <ion-card>
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
        </ion-card>
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
import { getSignalRService } from "@/composables/signalRInstance";
import { trashOutline, addOutline } from "ionicons/icons";
import HeaderComponent from "@/components/HeaderComponent.vue";

//store, route, params
const route = useRoute();
const params = route.params;
const shopStore = shoppingListStore();

const logStore = loginStore();

//shoppingList
const shoppingList = ref<ShoppingList | undefined>();

onMounted(async () => {
  getShoppingList();
  addSignalRListener();
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

//helper functions
const getShoppingList = () => {
  shoppingList.value = shopStore.getShoppingList(params.id as string);
};

const addSignalRListener = () => {
  const signalRService = getSignalRService();
  signalRService.addUpdateShoppingListListener(
    (updatedShoppingList: ShoppingList) => {
      if (
        logStore.userId &&
        (logStore.userId == updatedShoppingList.creatorUserId ||
          updatedShoppingList.eligibleUsers.includes(logStore.userId))
      ) {
        if (updatedShoppingList.lastUpdatedUser !== logStore.userId) {
          shoppingList.value = updatedShoppingList;
        }
      }
    }
  );
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
  --background: linear-gradient(
      rgba(255, 255, 255, 0.7),
      rgba(255, 255, 255, 0.2)
    ),
    url("../assets/live-background.png") no-repeat center center / cover;
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

ion-card {
background-color: var(--ion-color-tertiary);
}
</style>
