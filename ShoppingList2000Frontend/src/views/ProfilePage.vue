<template>
  <ion-page>
    <HeaderComponent title="Profile" />
    <ion-content>
      <ion-card>
        <ion-card-header>
          <ion-card-title>Information</ion-card-title>
        </ion-card-header>
        <ion-card-content>
          <p>Username: {{ userName }}</p>
          <p>Email: {{ userEmail }}</p>
          <p>Id: {{ userId }}</p>
        </ion-card-content>
      </ion-card>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
//imports
import HeaderComponent from "@/components/HeaderComponent.vue";
import { loginStore } from "@/store/loginStore";

//user
const logStore = loginStore();
const userName = logStore.displayName;
const userEmail = logStore.email;
const userId = logStore.userId;
</script>

<style scoped>
ion-content {
  --background: url("../assets/profile-background.png");
  --padding-bottom: 10px;
  --padding-top: 10px;
  --padding-left: 10px;
  --padding-right: 10px;
}

ion-card {
  background-image: url("../assets/profile-card.png");
  background-size: cover;
  background-position: center;
}

p {
  font-weight: bold;
}
</style>
