<template>
  <ion-app>
    <side-menu></side-menu>
    <ion-router-outlet />
  </ion-app>
</template>

<script setup lang="ts">
import { IonApp, IonRouterOutlet } from '@ionic/vue';
import SideMenu from "./views/SideMenu.vue";
</script>
