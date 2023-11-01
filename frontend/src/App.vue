<script lang="ts">
export default {
  data() {
    return {
      searchQuery: "",
    };
  },
};
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
        <router-link to="/upnooks" class="text-white hover:underline"
          >UpNooks</router-link
        >
        <router-link to="/login" class="text-white hover:underline"
          >Login</router-link
        >
      </div>
    </div>
  </nav>
  <router-view />
</template>
