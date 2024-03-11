<template>
    <div class="ion-page" id="main-content"></div>
    <ion-header :translucent="true">
      <ion-toolbar>
        <ion-buttons slot="start">
          <ion-menu-button color="primary"></ion-menu-button>
        </ion-buttons>
        <ion-title color="primary">{{ props.title }}</ion-title>
      </ion-toolbar>
    </ion-header>
  </template>


<script setup lang="ts">
//imports

import {IonToolbar, IonButtons, IonMenuButton } from "@ionic/vue";
import { defineProps } from "vue";

const props = defineProps({
  title: String,
});

</script>