<template>
  <ion-page>
    <ion-content class="ion-padding">
      <ion-title color="primary" class="login-title">Login</ion-title>
      <ion-input
        v-model="email"
        type="text"
        label="E-mail"
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
      <ion-item lines="none">
        <ion-text color="medium" slot="end">Forgot password?</ion-text>
      </ion-item>
      <ion-button expand="full" @click="login">Log In</ion-button>
      <ion-row class="center-text">
        <ion-col class="line"></ion-col>
        <ion-col size="auto">
          <ion-text color="medium">Or Log In with</ion-text>
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
      <ion-item lines="none" class>
        <ion-text class="center-text-only" color="medium"
          >Don't have an account yet?</ion-text
        >
      </ion-item>
      <ion-button color="light" expand="full" @click="register"
        >Register</ion-button
      >
    </ion-content>
  </ion-page>
</template>

<script setup lang="ts">
//imports
import { ref } from "vue";
import { loginStore } from "@/store/loginStore";
import { useRouter } from "vue-router";
import { loginUser } from "@/services/loginService";
import { IonCol, IonRow, IonText, IonItem } from "@ionic/vue";
import { logoFacebook, logoGoogle } from "ionicons/icons";
import { initializeApp } from "firebase/app";
import { getAuth, signInWithEmailAndPassword } from "firebase/auth";
import { firebaseConfig } from "@/config/firebaseConfig";

//firebase

const app = initializeApp(firebaseConfig);
const auth = getAuth(app);

//routing
const router = useRouter();
const register = () => {
  router.push("/register");
};

//data
const email = ref("");
const password = ref("");

//authentification
const store = loginStore();
const login = async () => {
  try {
    const response = await loginUser(email.value, password.value);
    console.log(response);
    const token = response.data.jwtToken;
    const userId = response.data.userUid;
    store.login(token, userId);

    console.log(auth);
    console.log(token);
    signInWithEmailAndPassword(auth, email.value, password.value)
      .then((userCredential) => {
        // Signed in
        const user = userCredential.user;
        console.log(user);
      })
      .catch((error) => {
        console.log(error);
        // ...
      });
    email.value = "";
    password.value = "";
    router.push("/home");
  } catch (error) {
    console.error(error);
  }
};
</script>

<style scoped>
ion-content {
  --background: linear-gradient(
      rgba(255, 255, 255, 0.7),
      rgba(255, 255, 255, 0.2)
    ),
    url("../assets/login-background.png") no-repeat center center / cover;
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
  margin-top: 60px;
}

.facebook-icon {
  margin-right: 10px;
  color: blue;
}

.google-icon {
  margin-right: 10px;
  color: red;
}
</style>
