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
          <ion-label>
            <!-- <ion-checkbox slot="start" v-model="item.checked" /> -->
            {{ item.name }}
          </ion-label>
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
      <ion-button @click="update">Update</ion-button>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
//imports
import { trashOutline, addOutline } from "ionicons/icons";
import { useRoute } from "vue-router";
import { ref, onMounted } from "vue";
import { shoppingListStore } from "@/store/shoppingListStore";
import { ShoppingList } from "@/models/ShoppingList";
import { loginStore } from "@/store/loginStore";
import { updateShoppingList } from "../services/shoppingListService";

//store, route, params
const store = loginStore();
const route = useRoute();
const params = route.params;
const shopStore = shoppingListStore();
const userId = ref(store.userId);

//shoppingList
const shoppingList = ref<ShoppingList | undefined>();

onMounted(() => {
  shoppingList.value = shopStore.getShoppingList(params.id as string);
});
const newItem = ref("");
const addItem = () => {
  if (newItem.value && shoppingList.value) {
    shoppingList.value.products.push({
      name: newItem.value,
      isPermanent: false,
      isChecked: false,
    });
    newItem.value = "";
  }
};
const removeItem = (index: number) => {
  if (shoppingList.value) {
    shoppingList.value.products.splice(index, 1);
  }
};

const update = async () => {
  if (shoppingList.value && userId.value) {
    try {
      shoppingList.value.lastUpdatedUser = userId.value;
      await updateShoppingList(shoppingList.value, userId.value).then(
        (response) => {
          console.log(response);
        }
      );
    } catch (error) {
      console.error(error);
    }
  }
};
</script>
