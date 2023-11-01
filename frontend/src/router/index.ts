import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/login",
    name: "Login Page",
    component: () => import("../views/LoginView.vue"),
  },
  {
    path: "/",
    name: "Dashboard",
    component: () => import("../views/HomeView.vue"),
  },
  {
    path: "/register",
    name: "Register Page",
    component: () => import("../views/RegisterView.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
