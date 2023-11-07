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
    ...mapActions(usePostStore, ["fetchPostsInVoteNook"]),
    redirectToCreatePost() {
      this.$router.push("/posts/create");
    },
  },
  created() {
    this.fetchPostsInVoteNook(this.$route.params.id);
  },
});
</script>
<template>
  <div class="min-h-screen flex justify-center bg-gray-200 p-4">
    <div class="w-full max-w-screen-md">
      <button
        @click="redirectToCreatePost"
        class="w-full bg-white text-gray-600 rounded-lg px-4 py-2 font-medium hover:bg-gray-100 focus:outline-none"
      >
        Create Post
      </button>
    </div>
    <div class="max-w-screen-md">
      <div
        v-for="post in posts"
        :key="post.id"
        class="w-full flex flex-col items-center"
      >
        <PostCard :post="post" />
      </div>
    </div>
  </div>
</template>
