<script lang="ts">
import { defineComponent } from "vue";
import PostCard from "@/components/PostCard.vue";
import { mapState, mapActions } from "pinia";
import { usePostStore } from "../stores/postStore";

export default defineComponent({
  components: {
    PostCard,
  },
  computed: {
    ...mapState(usePostStore, ["posts"]),
  },
  methods: {
    ...mapActions(usePostStore, ["fetchPosts"]),
    redirectToCreatePost() {
      this.$router.push("/posts/create");
    },
  },
  watch: {
    "$route.query.query": {
      handler(newValue, oldValue) {
        this.fetchPosts(newValue);
      },
      immediate: true,
    },
  },
});
</script>
<template>
  <div
    class="w-full min-h-screen p-4 bg-gray-900 flex items-center justify-center"
  >
    <div
      class="w-full max-w-screen-xl flex flex-col mx-auto px-4 space-y-6 items-center"
    >
      <button
        @click="redirectToCreatePost"
        class="bg-blue-600 self-end text-white rounded-lg px-4 py-2 font-medium hover:bg-blue-700 focus:outline-none"
      >
        Create Post
      </button>

      <div class="w-full max-w-screen-md">
        <PostCard
          v-for="post in posts"
          :key="post.id"
          :post="post"
          class="bg-white rounded-lg shadow-md p-4 my-4"
        />
      </div>
    </div>
  </div>
</template>
