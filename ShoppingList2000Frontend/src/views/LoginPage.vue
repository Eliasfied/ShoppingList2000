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
          <ion-button @click="facebookLogin" color="white" expand="full"
            ><ion-icon class="facebook-icon" :icon="logoFacebook"></ion-icon
            ><ion-text color="dark">Facebook</ion-text></ion-button
          >
        </ion-col>
        <ion-col>
          <ion-button @click="googleLogin" color="white" expand="full"
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
import { signIn, signInWithGoogle, signInWithFacebook } from "../services/fireBaseService";

//firebase

//routing
const router = useRouter();
const register = () => {
  router.push("/register");
};

//data
const email = ref("");
const password = ref("");

const store = loginStore();
const handleLoginResponse = (response: any) => {
  const token = response.data.jwtToken;
  const userId = response.data.userUid;
  const userName = response.data.userName;
  store.login(token, userId, userName, email.value);
  console.log(token);
};

const handleFirebaseSignIn = (userCredential: any) => {
  const user = userCredential.user;
  console.log(user);
};

const handleError = (error: any) => {
  console.error(error);
};

const login = async () => {
  try {
    const response = await loginUser(email.value, password.value);
    handleLoginResponse(response);
    await signIn(email.value, password.value)
      .then(handleFirebaseSignIn)
      .catch(handleError);
    email.value = "";
    password.value = "";
    router.push("/home");
  } catch (error) {
    handleError(error);
  }
};


const googleLogin = async () => {
  try {
   const credentials = await signInWithGoogle();
    console.log(credentials);
    if (credentials!=null) {

      store.login(credentials.token, credentials.userId , credentials.userName as string, credentials.userEmail as string);
    }
    router.push("/home");

  } catch (error) {
    handleError(error);
  }
};

const facebookLogin = async () => {
  try {
   const credentials = await signInWithFacebook();
    console.log(credentials);
    if (credentials!=null) {

      store.login(credentials.token, credentials.userId , credentials.userName as string, credentials.userEmail as string);
    }
    router.push("/home");

  } catch (error) {
    handleError(error);
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
