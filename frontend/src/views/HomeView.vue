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
  <div class="w-full h-full min-h-screen p-4 bg-gray-900">
    <div class="w-screen max-w-xl mx-auto space-y-4">
      <PostCard
        v-for="post in posts"
        :key="post.id"
        :post="post"
        class="bg-white rounded shadow p-4"
      />
    </div>
  </div>
</template>
