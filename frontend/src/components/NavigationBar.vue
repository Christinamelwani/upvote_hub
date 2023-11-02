<script lang="ts">
import { mapState, mapActions } from "pinia";
import { defineComponent } from "vue";
import { useUserStore } from "@/stores/userStore";

export default defineComponent({
  name: "navigation-bar",
  computed: {
    ...mapState(useUserStore, ["user", "loggedIn"]),
  },
  methods: {
    ...mapActions(useUserStore, ["fetchUser", "logout"]),
  },
  data() {
    return {
      searchQuery: "",
    };
  },
  created() {
    this.fetchUser();
  },
});
</script>

<template>
  <nav class="bg-orange-500 p-4">
    <div class="container mx-auto flex items-center justify-between">
      <router-link to="/" class="text-white text-2xl font-bold"
        >UpvoteNook</router-link
      >
      <form
        class="w-1/2"
        @submit.prevent="$router.push(`/?query=${searchQuery}`)"
      >
        <input
          v-model="searchQuery"
          type="text"
          placeholder="Search UpvoteNook..."
          class="w-full py-2 px-4 bg-white rounded-full focus:outline-none text-gray-800"
        />
      </form>
      <div class="space-x-4">
        <router-link to="/votenooks" class="text-white hover:underline"
          >VoteNooks</router-link
        >
        <router-link
          v-if="!loggedIn"
          to="/login"
          class="text-white hover:underline"
          >Login</router-link
        >
        <span
          v-else
          @click="logout"
          class="text-white cursor-pointer hover:underline"
        >
          Logout
        </span>
      </div>
    </div>
  </nav>
</template>
