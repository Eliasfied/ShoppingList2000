<template>
  <ion-list>
    <ion-item>
      <ion-input
        v-if="shoppingListNew"
        label="ShoppingList"
        label-placement="stacked"
        placeholder="Enter ShoppingList Name"
        v-model="shoppingListNew.shoppingListName"
        type="text"
      ></ion-input>
    </ion-item>

    <ion-list>
      <ion-item v-for="(item, index) in shoppingListNew?.products" :key="index">
        <ion-label>
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
    <ion-list>
      <ion-item>
        <ion-input v-model="productName" placeholder="Add Item"></ion-input>
        <ion-button @click="addProductToShoppingList">
          <ion-icon :icon="addOutline"></ion-icon>
        </ion-button>
      </ion-item>
    </ion-list>
  </ion-list>
  <ion-button @click="createShoppingList">Create</ion-button>
</template>

<script setup lang="ts">
//imports
import { trashOutline, addOutline } from "ionicons/icons";
import { ShoppingList } from "@/models/ShoppingList";
import { defineProps } from "vue";
import { Product } from "../models/Product";
import { ref } from "vue";
import { defineEmits } from "vue";

const productName = ref("");

//emit, props
const emit = defineEmits(["create"]);
const props = defineProps({
  shoppingList: ShoppingList,
});

//shoppingList
const shoppingListNew = ref(props.shoppingList);
const createShoppingList = () => {
  emit("create", shoppingListNew.value);
  shoppingListNew.value = new ShoppingList("", "", [], [], "");
};

const addProductToShoppingList = () => {
  if (shoppingListNew.value) {
    const product = new Product(productName.value, true, false);
    shoppingListNew.value.products.push(product);
    productName.value = "";
  }
};

const removeItem = (index: number) => {
  if (shoppingListNew.value) {
    shoppingListNew.value.products.splice(index, 1);
  }
};
</script>
