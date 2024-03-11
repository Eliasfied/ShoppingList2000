<template>
  <ion-page>
    <HeaderComponent title="Meine Listen" />
    <ion-content :fullscreen="true" color="tertiary">
      <div class="grid-style-workouts">
        <div class="workout-list">
          <ul>
            <li v-for="(list, index) in shoppingLists" :key="index">
              <ion-card @click="goToShoppingList(list.shoppingListId)">
              <div class="grid-style-li">
                <div class="list-name">
                 <ion-label>{{ list.shoppingListName }}</ion-label>
                </div>
                <div class="list-edit">
                 <ion-icon @click.stop="goToEditShoppingList(list.shoppingListId)" class="edit-icon" :icon="menuOutline"></ion-icon>

                </div>
                <div class="list-items">
                 <ion-label>{{ countItems }}</ion-label>

                </div>
                <div class="list-users">
                 <ion-label>{{ shoppingListUsers }}</ion-label>

                </div>
              </div>
              </ion-card>
            </li>
          </ul>
        </div>

      
      </div>
      <ion-fab vertical="bottom" horizontal="end" slot="fixed">
      <ion-fab-button  @click="goToCreateShoppingList">
        <ion-icon class="add-icon" :icon="addOutline"></ion-icon>
      </ion-fab-button>
    </ion-fab>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
//imports
import { onMounted, ref, watch, Ref } from "vue";
import { useRouter } from "vue-router";
import { loginStore } from "@/store/loginStore";
import { ShoppingList } from "@/models/ShoppingList";
import { shoppingListStore } from "@/store/shoppingListStore";
import { getShoppingLists } from "../services/shoppingListService";
import HeaderComponent from "@/components/HeaderComponent.vue";
import {addOutline, menuOutline} from "ionicons/icons";
import { IonFab, IonFabButton } from "@ionic/vue";

// store, login, router
const logStore = loginStore();
const isLoggedIn = ref(false);
const userId = ref("") as Ref<string> | Ref<null>;

const router = useRouter();
// const logout = () => {
//   logStore.logout();
//   router.push("/login");
// };

onMounted(async () => {
  console.log("bin im mounted von homepage");
  await logStore.checkLoginStatus();
  isLoggedIn.value = logStore.isLoggedIn;
  userId.value = logStore.userId;

  if (isLoggedIn.value) {
    get(userId.value);
  }
});

const shopStore = shoppingListStore();

// shoppingLists
const shoppingLists = ref([]) as Ref<ShoppingList[]>;

const countItems = "14 Items";
const shoppingListUsers = "Beate, peter und dieter";
const get = async (userId: string | null) => {
  try {
    await getShoppingLists(userId).then((response) => {
      console.log(response.data);
      shoppingLists.value = response.data;
      shopStore.shoppingLists = response.data;
    });
  } catch (error) {
    console.log(error);
  }
};

const goToShoppingList = (shoppingListId: string | null | undefined) => {
  if (shoppingListId) {
    router.push("/liveShoppingList/" + shoppingListId);
  } else {
    console.log("shoppingListId is undefined");
  }
};


const goToEditShoppingList = (shoppingListId: string | null | undefined) => {
  if (shoppingListId) {
    router.push("/editShoppingList/" + shoppingListId);
  } else {
    console.log("shoppingListId is undefined");
  }
};

const goToCreateShoppingList = () => {
  router.push("/createShoppingList");
};

//watchers
watch(
  () => shopStore.shoppingLists,
  (newShoppingLists) => {
    shoppingLists.value = newShoppingLists.filter(
      (list) => list !== undefined
    ) as ShoppingList[];
  },
  { deep: true }
);

watch(
  () => logStore.isLoggedIn,
  (newIsLoggedIn) => {
    isLoggedIn.value = newIsLoggedIn;
  }
);

watch(
  () => logStore.userId,
  (newUserId) => {
    userId.value = newUserId;
    get(newUserId);
  }
);
</script>

<style scoped>
.grid-style-workouts {
  display: grid;
  height: 97.5%;
  grid-template-rows: [row1-start] 5% [row1-end] 95% [row2-start];
}

.workout-list {
  width: 100%;
  grid-row: row1-end / row2-start;
}

.grid-style-li {
  display: grid;
  height: 100%;
  grid-template-rows: [row1-start] 35% [row1-end] 15% [row2-start] 30% [row2-end] 20% [row3-start];
  grid-template-columns: [column1-start] 80% [column1-end] 20% [column2-start];
  padding: 2.5%;
}

.list-name {
  grid-row: row1-start / row1-end;
  grid-column: column1-start / column1-end;
  display: flex;
  align-items: center;
}

.list-edit {
  grid-row: row1-start / row1-end;
  grid-column: column1-end / column2-start;
  display: flex;
  align-items: center;
  justify-content: center;
}

.list-items {
  grid-row: row1-end / row2-start;
  grid-column: column1-start / column1-end;
  display: flex;
  align-items: start;
}

.list-users {
  grid-row: row2-start / row2-end;
  grid-column: column1-start / column1-end;
  display: flex;
  align-items: end;
}


li {
  width: 100%;
  height: 22.5%;
}

ul {
  width: 100%;
  height: 100%;
  list-style: none;
  padding: 0;
  overflow-y: auto;
}

ion-card {
  height: 100%;
  background-color: #bce3f7;
}

ion-fab {
  position: fixed;
  bottom: 10%; 
  left: 50%;
  transform: translateX(-50%);
  
}

ion-fab-button {
  width: 100px;
  height: 100px;
}

.add-icon {
  height: 50px;
  width: 50px;
}

.edit-icon {
  height: 30px;
  width: 30px;
}


</style>
