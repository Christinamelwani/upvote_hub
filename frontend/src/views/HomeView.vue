<script lang="ts">
import { defineComponent } from "vue";
import PostCard from "@/components/PostCard.vue";
import axios from "axios";

export default defineComponent({
  components: {
    PostCard,
  },
  created() {
    this.fetchArticle();
  },
  methods: {
    async fetchArticle() {
      try {
        let query = this.$route.query.query;
        let response;
        if (query) {
          response = await axios.get(
            `http://localhost:5066/post/search?query=${query}`
          );
        } else {
          response = await axios.get("http://localhost:5066/post");
        }

        this.posts = response?.data;
      } catch (err) {
        console.log(err);
      }
    },
  },
  data() {
    return {
      posts: [],
    };
  },
  watch: {
    "$route.query.query": "fetchArticle",
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
