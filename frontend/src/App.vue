<script lang="ts">
import NavigationBar from "./components/NavigationBar.vue";

export default {
  components: {
    NavigationBar,
  },
};
</script>

<template>
  <NavigationBar :key="$route.path" />
  <router-view />
</template>
