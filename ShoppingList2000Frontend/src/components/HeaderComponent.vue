<template>
    <div class="ion-page" id="main-content"></div>
    <ion-header :translucent="true">
      <ion-toolbar>
        <ion-buttons slot="start">
          <ion-menu-button color="primary"></ion-menu-button>
        </ion-buttons>
        <ion-buttons slot="end">
          <ion-button @click="goToNotifications" color="primary">
            <ion-icon :icon="notificationsOutline" :class="{ 'has-notifications': hasUnreadNotifications }"></ion-icon>
          </ion-button>
        </ion-buttons>
        <ion-title color="primary">{{ props.title }}</ion-title>
      </ion-toolbar>
    </ion-header>
  </template>


<script setup lang="ts">
//imports

import {IonToolbar, IonButtons, IonMenuButton } from "@ionic/vue";
import { defineProps } from "vue";
import { notificationsOutline } from "ionicons/icons";
import { useRouter } from "vue-router";
import useNotifications from "@/composables/useNotifications";

const props = defineProps({
  title: String,
});

const { hasUnreadNotifications } = useNotifications();
const router = useRouter();

const goToNotifications = ( ) => {
  router.push("/notifications");
};

</script>


<style scoped>
 
 ion-icon {
  margin-right: 40px;
 }

 ion-icon.has-notifications::after {
  content: "";
  position: absolute;
  top: 0;
  right: 0;
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background-color: red;
 }
</style>