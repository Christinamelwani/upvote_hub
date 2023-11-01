<script lang="ts">
import { defineComponent } from "vue";
import PostCard from "@/components/PostCard.vue";
import axios from "axios";

export default defineComponent({
  components: {
    PostCard,
  },
  async created() {
    const response = await axios.get(
      `http://localhost:5066/user/${this.$route.params.id}/posts`
    );
    this.posts = response.data;
  },
  data() {
    return {
      posts: [],
    };
  },
});
</script>
<template>
  <div class="w-full h-full min-h-screen p-4 bg-gray-900">
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
