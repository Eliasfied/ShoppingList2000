<template>
  <ion-page>
    <HeaderComponent title="My Lists" />
    <ion-content id="main-content">
      <div class="grid-style-lists">
        <div class="shopping-list">
          <ul>
            <li v-for="(list, index) in shoppingLists" :key="index">
              <ion-card
                class="card-background"
                @click="goToShoppingList(list.shoppingListId)"
              >
                <div class="grid-style-li">
                  <div class="list-name">
                    <ion-label>{{ list.shoppingListName }}</ion-label>
                  </div>
                  <div class="list-edit">
                    <ion-fab class="edit-fab" @click.stop>
                      <ion-fab-button class="edit-fab-button">
                        <ion-icon :icon="menuOutline"></ion-icon>
                      </ion-fab-button>
                      <ion-fab-list side="start">
                        <ion-fab-button
                          @click="deleteShoppinglist(index)"
                          class="edit-fab-button-list"
                        >
                          <ion-icon
                            color="danger"
                            :icon="trashOutline"
                          ></ion-icon>
                        </ion-fab-button>
                        <ion-fab-button
                          color="medium"
                          class="edit-fab-button-list"
                          @click.stop="openShareDialog(index)"
                        >
                          <ion-icon :icon="shareOutline"></ion-icon>
                        </ion-fab-button>
                      </ion-fab-list>
                    </ion-fab>
                  </div>
                  <div class="list-items">
                    <ion-label>{{ countItems(index) }} Items</ion-label>
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
        <ion-fab-button @click="goToCreateShoppingList">
          <ion-icon class="add-icon" :icon="addOutline"></ion-icon>
        </ion-fab-button>
      </ion-fab>
    </ion-content>

    <ion-alert
      :is-open="isShareDialogOpen"
      :header="'Share Shopping List'"
      :inputs="[
        {
          name: 'userId',
          type: 'text',
          placeholder: 'Enter user ID',
        },
      ]"
      :buttons="[
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: () => {
            isShareDialogOpen = false;
          },
        },
        {
          text: 'Share',
          handler: (data) => share(data.userId),
        },
      ]"
    ></ion-alert>
  </ion-page>
</template>

<script setup lang="ts">
//imports
import { onMounted, ref, watch, Ref, computed } from "vue";
import { useRouter } from "vue-router";
import { loginStore } from "@/store/loginStore";
import { ShoppingList } from "@/models/ShoppingList";
import { shoppingListStore } from "@/store/shoppingListStore";
import { getShoppingLists } from "../services/shoppingListService";
import { deleteShoppingList } from "../services/shoppingListService";
import { shareShoppinglist } from "../services/shoppingListService";
import HeaderComponent from "@/components/HeaderComponent.vue";
import {
  addOutline,
  menuOutline,
  trashOutline,
  shareOutline,
} from "ionicons/icons";
import { IonFab, IonFabButton, IonFabList, IonAlert } from "@ionic/vue";
import { onAuthChange } from "@/services/fireBaseService";
import useNotifications from "@/composables/useNotifications";
import { getAllNotifications } from "@/services/notificationService";
import { getSignalRService } from "@/composables/signalRInstance";


//notifications
const { setHasUnreadNotifications } = useNotifications();

// store, login, router
const logStore = loginStore();
const isLoggedIn = ref(false);
const userId = ref("") as Ref<string> | Ref<null>;
const router = useRouter();
const shopStore = shoppingListStore();




onMounted(async () => {
  onAuthChange(async (user: any) => {
    if (user) {
      await initializeUser(user);
      await initializeSignalRService();
      await checkNotifications();
    } else {
      logoutUser();
    }
  });
});

//Alert
const openShareDialog = (index: number) => {
  selectedIndex.value = index;
  isShareDialogOpen.value = true;
};

const selectedIndex = ref(0);

const isShareDialogOpen = ref(false);

const share = async (receiverEmail: string) => {
  shareShoppinglist(
    logStore.displayName as string,
    receiverEmail as string,
    shoppingLists.value[selectedIndex.value].shoppingListId as string
  );
};

// shoppingLists
const shoppingLists = ref([]) as Ref<ShoppingList[]>;

const countItems = computed(() => {
  return (index: any) => {
    return shoppingLists.value[index].products.length;
  };
});
const shoppingListUsers = "";
const getLists = async (userId: string | null) => {
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

const goToCreateShoppingList = () => {
  router.push("/createShoppingList");
};

const deleteShoppinglist = async (index: number) => {
  try {
    await deleteShoppingList(
      shoppingLists.value[index].shoppingListId,
      userId.value
    );
  } catch (error) {
    console.log(error);
  }
  shoppingLists.value.splice(index, 1);
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
    getLists(newUserId);
  }
);


// helper functions
const initializeUser = async (user: any) => {
  console.log(user);
  logStore.login(user.accessToken, user.uid, user.displayName);
  isLoggedIn.value = true;
  userId.value = logStore.userId;
  await getLists(userId.value);
};

const initializeSignalRService = async () => {
  const signalRService = getSignalRService();
  await signalRService.startConnection();
  signalRService.sharedShoppingListListener(() => {
    setHasUnreadNotifications(true);
  });
};

const checkNotifications = async () => {
  const response = await getAllNotifications(userId.value as string);
  const filteredResponse = response.data.filter(
    (notification: any) => notification.isAcknowledged === false
  );
  if (filteredResponse.length > 0) {
    setHasUnreadNotifications(true);
  } else {
    setHasUnreadNotifications(false);
  }
};

const logoutUser = () => {
  logStore.logout();
  isLoggedIn.value = false;
};
</script>

<style scoped>
ion-content {
  --background: linear-gradient(
      rgba(255, 255, 255, 0.5),
      rgba(255, 255, 255, 0.2)
    ),
    url("../assets/home-background.png") no-repeat center center / cover;
}

.grid-style-lists {
  display: grid;
  height: 97.5%;
  grid-template-rows: [row1-start] 1% [row1-end] 98% [row2-start];
}

.shopping-list {
  width: 100%;
  grid-row: row1-end / row2-start;
}

.grid-style-li {
  display: grid;
  height: 100%;
  grid-template-rows: [row1-start] 35% [row1-end] 15% [row2-start] 30% [row2-end] 20% [row3-start];
  grid-template-columns: [column1-start] 80% [column1-end] 20% [column2-start];
  padding: 3.5%;
}

.list-name {
  grid-row: row1-start / row1-end;
  grid-column: column1-start / column1-end;
  display: flex;
  align-items: center;
  color: black;
  font-family: Arial, sans-serif;
  font-size: 18px;
  font-weight: bold;
}

.list-edit {
  position: relative;
  grid-row: row1-start / row1-end;
  grid-column: column1-end / column2-start;
  display: flex;
  align-items: center;
  justify-content: center;
  color: black;
}

.edit-fab {
  position: absolute;
  top: 0;
  right: 0;
}

.edit-fab-button {
  width: 50px;
  height: 50px;
  --background: transparent;
  --box-shadow: none;
}

.edit-fab-button-list {
  width: 35px;
  height: 35px;
}

.list-items {
  grid-row: row1-end / row2-start;
  grid-column: column1-start / column1-end;
  display: flex;
  align-items: start;
  font-family: Arial, sans-serif;
  font-size: 14px;
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
  margin-top: 5%;
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

.card-background {
  background-image: url("../assets/list-background.png");
  background-size: cover;
  background-position: center;
}
</style>
