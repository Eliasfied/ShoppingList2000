import { createRouter, createWebHistory } from "@ionic/vue-router";
import { RouteRecordRaw } from "vue-router";
import HomePage from "../views/HomePage.vue";
import CreateShoppingListPage from "../views/CreateShoppingListPage.vue";
import LoginPage from "../views/LoginPage.vue";
import RegisterPage from "../views/RegisterPage.vue";
import EditShoppingListPage from "../views/EditShoppingListPage.vue";
import LiveShoppingList from "../views/LiveShoppingList.vue";

import { loginStore } from "@/store/loginStore";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    redirect: "/home",
    meta: { requiresAuth: true },
  },
  {
    path: "/home",
    name: "Home",
    component: HomePage,
    meta: { requiresAuth: true },
  },
  {
    path: "/createShoppingList",
    name: "CreateShoppingList",
    component: CreateShoppingListPage,
    meta: { requiresAuth: true },
  },
  {
    path: "/login",
    name: "Login",
    component: LoginPage,
  },
  {
    path: "/register",
    name: "Register",
    component: RegisterPage,
  },
  {
    path: "/editShoppinglist/:id",
    name: "EditShoppingList",
    component: EditShoppingListPage,
    meta: { requiresAuth: true },
  },
  {
    path: "/liveShoppinglist/:id",
    name: "LiveShoppingList",
    component: LiveShoppingList,
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const store = loginStore();
  await store.checkLoginStatus();
  console.log(store.isLoggedIn);
  if (
    to.matched.some((record) => record.meta.requiresAuth) &&
    !store.isLoggedIn
  ) {
    next({ name: "Login" });
  } else {
    next();
  }
});

export default router;
