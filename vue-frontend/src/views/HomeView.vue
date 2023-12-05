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
    class="w-full min-h-screen p-4 bg-[#DAE0E6] flex items-center justify-center"
  >
    <div
      class="w-full max-w-screen-xl flex flex-col mx-auto px-4 space-y-6 items-center"
    >
      <div class="w-full max-w-screen-md">
        <button
          @click="redirectToCreatePost"
          class="bg-white h-full w-full text-gray-600 rounded-lg px-4 py-2 font-medium hover:bg-gray-100 focus:outline-none"
        >
          Create Post
        </button>
      </div>

      <div class="w-full max-w-screen-md">
        <div
          v-for="post in posts"
          :key="post.id"
          class="flex flex-col items-center w-full"
        >
          <PostCard :post="post" />
        </div>
      </div>
    </div>
  </div>
</template>
