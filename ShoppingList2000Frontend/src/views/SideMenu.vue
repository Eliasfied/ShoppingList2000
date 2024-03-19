<template>
  <ion-menu content-id="main-content" color="tertiary">
    <ion-header>
      <ion-toolbar color="tertiary">
        <ion-title color="primary">Menu</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content color="tertiary">
      <ion-list color="tertiary">
        <ion-menu-toggle>
          <ion-item router-link="/home" color="tertiary"
            ><ion-icon
              :class="{ active: isActive('/home') }"
              slot="start"
              :icon="homeOutline"
              color="primary"
            ></ion-icon>
            <p :class="{ active: isActive('/home') }">Homepage</p>
          </ion-item>
        </ion-menu-toggle>
      </ion-list>

      <ion-list class="bottom-list">
        <ion-menu-toggle v-if="isLoggedIn">
          <ion-item router-link="/profile" color="tertiary"
            ><ion-icon
              :class="{ active: isActive('/profile') }"
              slot="start"
              :icon="personOutline"
            ></ion-icon>
            <p :class="{ active: isActive('/profile') }">Profile</p>
          </ion-item>
          <ion-item @click="logoutUser" color="tertiary"
            ><ion-icon slot="start" :icon="logOutOutline"></ion-icon>
            <p>Logout</p>
          </ion-item>
        </ion-menu-toggle>

        <ion-menu-toggle v-else>
          <ion-item router-link="/profile" color="tertiary"
            ><ion-icon
              :class="{ active: isActive('/profile') }"
              slot="start"
              :icon="logInOutline"
            ></ion-icon>
            <p>Login</p>
          </ion-item>
        </ion-menu-toggle>
      </ion-list>
    </ion-content>
  </ion-menu>
  <ion-router-outlet id="main-content"></ion-router-outlet>
</template>

<script setup lang="ts">
//imports
import { IonMenu, IonToolbar, IonTitle, IonMenuToggle, IonRouterOutlet } from "@ionic/vue";
import {
  homeOutline,
  personOutline,
  logOutOutline,
  logInOutline,
} from "ionicons/icons";
import { useRouter, useRoute } from "vue-router";
import { loginStore } from "@/store/loginStore";
import { ref, onMounted, watch } from "vue";
import { logout } from "@/services/fireBaseService";

const logStore = loginStore();
const router = useRouter();
const route = useRoute();
const isActive = (path: any) => {
  return path === route.fullPath;
};

const isLoggedIn = ref(false);

watch(() => logStore.isLoggedIn, (newVal: boolean) => {
  isLoggedIn.value = newVal;
});

onMounted(async () => {
  console.log("bin in onMounted drin");
  await logStore.checkLoginStatus();
  isLoggedIn.value = logStore.isLoggedIn;
});

function logoutUser() {
  logStore.logout();
  isLoggedIn.value = logStore.isLoggedIn;
  logout();
  router.push("/login");
}
</script>
<style scoped>

p {
  color: var(--ion-color-primary);
}

ion-icon {
  color: var(--ion-color-primary);
}

ion-list {
  background-color: var(--ion-color-tertiary);
}

.routerLink {
  text-decoration: none;
}

ion-toolbar {
  border-bottom: 2px solid black;
}

.active {
  color: var(--ion-color-primary);
}

.bottom-list {
  position: absolute;
  bottom: 0;
  width: 100%;
}
</style>
