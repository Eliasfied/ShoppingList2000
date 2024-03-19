<template>
  <ion-page>
    <HeaderComponent title="Notifications" />
    <ion-content color="tertiary">
      <ion-card
        @click="acknowledge(notification)"
        v-for="(notification, index) in notifications"
        :key="index"
      >
        <ion-card-content class="notification-card">
          <div class="notification-dot"></div>
          <div class="notification-header">
            <ion-avatar>
              <img alt="Silhouette of a person's head" src="https://ionicframework.com/docs/img/demos/avatar.svg" />
            </ion-avatar>
            <div class="notification-content card-content-right-margin">
              <p class="notification-text">{{ notification.text }}</p>
            </div>
          </div>
        </ion-card-content>
      </ion-card>
    </ion-content>
  </ion-page>
</template>
<script setup lang="ts">
//imports
import { getAllNotifications } from "@/services/notificationService";
import { acknowledgeNotification } from "@/services/notificationService";
import { ref, onMounted } from "vue";
import { loginStore } from "@/store/loginStore";
import HeaderComponent from "@/components/HeaderComponent.vue";
import { IonCardContent, IonAvatar } from "@ionic/vue";
import useNotifications from "@/composables/useNotifications";
import useNotificationActions from "@/composables/useNotificationActions";
import { useRouter } from "vue-router";
import { shoppingListStore } from "@/store/shoppingListStore";
import { getSignalRService } from "@/composables/signalRInstance";

//loginStore
const router = useRouter();
const shopStore = shoppingListStore();
const logStore = loginStore();

//notifications
const { performNotificationAction } = useNotificationActions();
const { setHasUnreadNotifications } = useNotifications();
const acknowledge = async (notificationValue: any) => {
  try {
    await acknowledgeNotification(notificationValue.id);
    notifications.value = notifications.value.filter(
      (notification: any) => notification.id !== notificationValue.id
    );
    if (notifications.value.length === 0) {
      setHasUnreadNotifications(false);
    }
    await performNotificationAction(
      notificationValue,
      router,
      shopStore,
      logStore
    );
  } catch (error) {
    console.log(error);
  }
};

const notifications = ref([]) as any;

//mounted
onMounted(async () => {
  await getNotifications();

  const signalRService = getSignalRService();
  signalRService.sharedShoppingListListener(async () => {
    await getNotifications();
  });
});

// helper functions
const getNotifications = async () => {
  await getAllNotifications(logStore.userId as string).then((response) => {
    notifications.value = response.data.filter(
      (notification: any) => notification.isAcknowledged === false
    );
  });
};
</script>

<style scoped>
ion-card {
  margin: 0;
  border: none;
  background: linear-gradient(rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0.3)), url("../assets/notifications-card-background.png");
  background-size: cover;
  background-position: center;
}

.notification-card {
  display: flex;
  align-items: center;
}

.notification-dot {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background-color: red;
  margin-right: 10px;
}

.notification-header {
  display: flex;
  align-items: center;
}

.notification-text {
  color: black;
  font-weight: bold;
}

ion-avatar {
  margin-right: 10px;
  margin-left: 10px;
}


</style>
