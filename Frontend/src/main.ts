import { createApp, markRaw } from "vue";
import { createPinia } from "pinia";
import App from "./App.vue";
import VueSweetalert2 from "vue-sweetalert2";
import "sweetalert2/dist/sweetalert2.min.css";
import "./index.css";
import router from "./router";

const pinia = createPinia();
pinia.use(({ store }) => {
  store.router = markRaw(router);
});

createApp(App).use(router).use(VueSweetalert2).use(pinia).mount("#app");
