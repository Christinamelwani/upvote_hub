/* eslint-disable */
import { Router } from "vue-router";
declare module "*.vue" {
  import type { DefineComponent } from "vue";
  const component: DefineComponent<{}, {}, any>;
  export default component;
}
declare module "pinia" {
  export interface PiniaCustomProperties {
    router: Router;
  }
}
