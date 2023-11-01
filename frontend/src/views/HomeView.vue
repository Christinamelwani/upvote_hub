<script lang="ts">
import { defineComponent } from "vue";
import PostCard from "@/components/PostCard.vue";
import axios from "axios";

export default defineComponent({
  components: {
    PostCard,
  },
  async created() {
    const response = await axios.get("http://localhost:5066/post");
    this.posts = response.data;
    console.log(this.posts);
  },
  data() {
    return {
      posts: [],
    };
  },
});
</script>
<template>
  <div class="w-full p-4">
    <div class="w-screen max-w-xl mx-auto space-y-4">
      <PostCard
        v-for="post in posts"
        :key="post"
        :post="post"
        class="bg-white rounded shadow p-4"
      />
    </div>
  </div>
</template>
