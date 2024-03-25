<template>
  <ion-page class="page-background">
    <HeaderComponent :title="shoppingList?.shoppingListName" />
    <ion-content>
      <ion-card>
        <ion-grid>
          <ion-row>
            <ion-col size="10">
              <div class="progress-container">
                <ion-progress-bar :value="progress"></ion-progress-bar>
                <p class="progress-text">{{ checkedCount }}/{{ totalCount }}</p>
              </div>
            </ion-col>
            <ion-col size="2" class="icon-container">
              <ion-icon
                :icon="refreshOutline"
                @click="resetList"
                class="reset-icon"
              ></ion-icon>
            </ion-col>
          </ion-row>
        </ion-grid>
      </ion-card>
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
            <p class="counter-onlist">{{ itemCountInList(index) }}</p>

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
          <ion-icon
            :icon="addOutline"
            @click="incrementCount"
            class="counter-icon"
          ></ion-icon>
          <p class="counter-text">{{ itemCount }}</p>
          <ion-icon
            :icon="removeOutline"
            @click="decrementCount"
            class="counter-icon"
          ></ion-icon>
          <ion-button class="add-button" @click="addItem">
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
import { ref, onMounted, computed } from "vue";
import { shoppingListStore } from "@/store/shoppingListStore";
import { ShoppingList } from "@/models/ShoppingList";
import {
  IonCheckbox,
  IonRow,
  IonCol,
  IonGrid,
  IonProgressBar,
} from "@ionic/vue";
import { updateShoppingList } from "@/services/shoppingListService";
import { loginStore } from "@/store/loginStore";
import { getSignalRService } from "@/composables/signalRInstance";
import {
  trashOutline,
  addOutline,
  refreshOutline,
  removeOutline,
} from "ionicons/icons";
import HeaderComponent from "@/components/HeaderComponent.vue";

//store, route, params
const route = useRoute();
const params = route.params;
const shopStore = shoppingListStore();

const logStore = loginStore();

//shoppingList
const shoppingList = ref<ShoppingList | undefined>();

const resetList = async () => {
  if (shoppingList.value) {
    shoppingList.value.products.forEach((product) => {
      product.isChecked = false;
    });
    await update();
  }
};

onMounted(async () => {
  getShoppingList();
  addSignalRListener();
});

const itemCountInList = (index: number) => {
  return shoppingList.value?.products[index].count == 1
    ? null
    : shoppingList.value?.products[index].count + "x";
};

const newItem = ref("");
const addItem = async () => {
  if (newItem.value && shoppingList.value) {
    shoppingList.value.products.push({
      name: newItem.value,
      isPermanent: false,
      isChecked: false,
      count: itemCount.value,
    });
    newItem.value = "";
    itemCount.value = 1;
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

//progress bar

const itemCount = ref(1);

const incrementCount = () => {
  itemCount.value++;
};

const decrementCount = () => {
  if (itemCount.value > 1) {
    itemCount.value--;
  }
};

const checkedCount = computed(() =>
  shoppingList.value
    ? shoppingList.value.products.filter((product) => product.isChecked).length
    : 0
);
const totalCount = computed(() =>
  shoppingList.value ? shoppingList.value.products.length : 0
);
const progress = computed(() =>
  totalCount.value ? checkedCount.value / totalCount.value : 0
);

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

.reset-icon {
  font-size: 30px;
}

.progress-container {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.progress-text {
  margin-left: 10px;
  font-weight: bold;
}

.icon-container {
  display: flex;
  align-items: center;
  justify-content: center;
}

.counter-icon {
  font-size: 30px;
}

.add-button {
  margin-left: 40px;
}

.counter-text {
  font-size: 20px;
  margin: 0 10px;
}

.counter-onlist {
  margin-left: 20px;
  margin-right: 20px;
}
</style>
