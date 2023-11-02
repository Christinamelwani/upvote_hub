import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/login",
    name: "Login Page",
    component: () => import("../views/LoginView.vue"),
  },
  {
    path: "/register",
    name: "Register Page",
    component: () => import("../views/RegisterView.vue"),
  },
  {
    path: "/",
    name: "Dashboard",
    component: () => import("../views/HomeView.vue"),
  },
  {
    path: "/users/:id",
    name: "UserPage",
    component: () => import("../views/UserView.vue"),
  },
  {
    path: "/posts/:id",
    name: "PostDetailPage",
    component: () => import("../views/PostDetailView.vue"),
  },
  {
    path: "/votenooks",
    name: "VoteNooksPage",
    component: () => import("../views/VoteNooksView.vue"),
  },
  {
    path: "/votenooks/:id",
    name: "VoteNookPage",
    component: () => import("../views/VoteNookView.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
