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
  },
  created() {
    this.fetchPostsInVoteNook(this.$route.params.id);
  },
});
</script>
<template>
  <div
    class="w-full min-h-screen p-4 bg-[#DAE0E6] flex items-center justify-center"
  >
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
</template>
