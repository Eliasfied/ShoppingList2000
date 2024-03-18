<template>
  <ion-page>
    <HeaderComponent title="Notifications" />
    <ion-content>
      <ion-card
        @click="acknowledge(notification.id)"
        v-for="(notification, index) in notifications"
        :key="index"
      >
        <ion-card-content class="notification-card">
          <div class="notification-content">
            <p>Receiver ID: {{ notification.receiverId }}</p>
            <p>Date: {{ notification.date }}</p>
            <p>Text: {{ notification.text }}</p>
          </div>
          <div class="notification-dot"></div>
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
import { IonCardContent } from "@ionic/vue";
import useNotifications from "@/composables/useNotifications";

//loginStore
const user = loginStore();

//notifications
const { setHasUnreadNotifications } = useNotifications();
const acknowledge = async (notificationId: string) => {
  try {
    await acknowledgeNotification(notificationId);
    notifications.value = notifications.value.filter(
      (notification: any) => notification.id !== notificationId
    );
    if (notifications.value.length === 0) {
      setHasUnreadNotifications(false);
    }
  } catch (error) {
    console.log(error);
  }
};

const notifications = ref([]) as any;

onMounted(async () => {
  const response = await getAllNotifications(user.userId as string);
  notifications.value = response.data.filter(
    (notification: any) => notification.isAcknowledged === false
  );
  console.log(notifications.value);
});
</script>

<style scoped>
ion-card {
  margin: 0;
  border: none;
}

.notification-card {
  position: relative;
  margin: 0;
  border: none;
}

.notification-content {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
}

.notification-dot {
  position: absolute;
  top: 10px;
  right: 10px;
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background-color: red;
  margin: 10px;
}
</style>
