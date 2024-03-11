import { defineStore } from "pinia";
import { useTokenValidation } from "../composables/UseTokenValidation";
export const loginStore = defineStore({
  id: "user",
  state: () => ({
    isLoggedIn: false as boolean,
    jwt: null as string | null,
    userId: null as string | null,
  }),
  actions: {
    login(jwt: string, userId: string) {
      localStorage.setItem("jwt", jwt);
      localStorage.setItem("userId", userId);
      this.jwt = jwt;
      this.userId = userId;
      this.isLoggedIn = true;
    },
    logout() {
      this.jwt = null;
      this.userId = null;
      this.isLoggedIn = false;
      localStorage.removeItem("jwt");
      localStorage.removeItem("userId");
    },
    async checkLoginStatus() {
      console.log("checkLoginStatus");
      const jwt = localStorage.getItem("jwt");
      const userId = localStorage.getItem("userId");
      if (jwt && userId) {
        if (await useTokenValidation(jwt)) {
          this.login(jwt, userId);
        } else {
          console.log("nix im token logge aus");
          // this.logout();
        }
      }
    },
  },
});
