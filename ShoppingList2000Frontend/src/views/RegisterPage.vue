<template>
  <ion-page>
    <ion-content class="ion-padding">
      <ion-title color="primary" class="login-title">Registration</ion-title>
      <ion-input
        v-model="name"
        type="text"
        label="Username"
        label-placement="floating"
        fill="outline"
        placeholder="Enter your Name"
      ></ion-input>
      <br />
      <ion-input
        v-model="email"
        type="text"
        label="E-Mail"
        label-placement="floating"
        fill="outline"
        placeholder="Enter your E-Mail"
      ></ion-input>
      <br />
      <ion-input
        v-model="password"
        type="password"
        label="Password"
        label-placement="floating"
        fill="outline"
        placeholder="Enter your Password"
      ></ion-input>
      <br>
      <ion-button expand="full" @click="register">Register</ion-button>
      <ion-row class="center-text">
        <ion-col class="line"></ion-col>
        <ion-col size="auto">
          <ion-text color="medium">Or Login with</ion-text>
        </ion-col>
        <ion-col class="line"></ion-col>
      </ion-row>

      <ion-row>
        <ion-col>
          <ion-button color="white" expand="full"
            ><ion-icon class="facebook-icon" :icon="logoFacebook"></ion-icon
            ><ion-text color="dark">Facebook</ion-text></ion-button
          >
        </ion-col>
        <ion-col>
          <ion-button color="white" expand="full"
            ><ion-icon class="google-icon" :icon="logoGoogle"></ion-icon
            ><ion-text color="dark">Google</ion-text></ion-button
          >
        </ion-col>
      </ion-row>
      <ion-item lines="none" class="login-text">
    <ion-text class="center-text-only" color="medium">
      Already have an Account? 
      <router-link to="/login" class="login-link">Login here!</router-link>
    </ion-text>
  </ion-item>
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
//imports
import { ref } from "vue";
import { useRouter } from "vue-router";
import { registerUser } from "@/services/loginService";
import { logoFacebook, logoGoogle } from "ionicons/icons";
import { IonText } from "@ionic/vue";


//router
const router = useRouter();

//data, register
const name = ref("");
const email = ref("");
const password = ref("");

const handleRegisterResponse = (response: any) => {
  console.log(response);
  name.value = "";
  email.value = "";
  password.value = "";
  router.push("/login");
};

const handleError = (error: any) => {
  console.error(error);
};

const register = async () => {
  try {
    console.log(`Email: ${email.value}, Password: ${password.value}, Name: ${name.value}`);
    const response = await registerUser(
      email.value,
      password.value,
      name.value
    );
    handleRegisterResponse(response);
  } catch (error) {
    handleError(error);
  }
};
</script>

<style scoped>

ion-content {
  --background: linear-gradient(rgba(255, 255, 255, 0.7), rgba(255, 255, 255, 0.2)), url("../assets/register-background.png") no-repeat center center / cover;
}

ion-item {
  --ion-background-color: transparent !important;
}


.center-text {
  display: flex;
  align-items: center;
}
.line {
  border-top: 1px solid grey;
  margin: 35px 10px;
  padding: 0;
}

.center-text-only {
  display: flex;
  justify-content: center;
  width: 100%;
}

.login-title {
  font-size: 3em;
  text-align: center;
  margin-bottom: 60px;
  margin-top: 60px
}

.facebook-icon {
  margin-right: 10px;
  color: blue;
}

.google-icon {
  margin-right: 10px;
  color: red;
}

.login-text {
  font-size: 0.9em; 
  margin-top: 20px; 
}

.login-link {
  text-decoration: underline;
}
</style>
