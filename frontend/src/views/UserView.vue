<script lang="ts">
import { defineComponent } from "vue";
import PostCard from "@/components/PostCard.vue";
import CommentCard from "@/components/CommentCard.vue";
import { mapState, mapActions } from "pinia";
import { usePostStore } from "../stores/postStore";
import { useCommentStore } from "../stores/commentStore";

export default defineComponent({
  components: {
    PostCard,
    CommentCard,
  },
  computed: {
    ...mapState(usePostStore, ["posts"]),
    ...mapState(useCommentStore, ["comments"]),
  },
  methods: {
    ...mapActions(usePostStore, ["fetchPostsByUser"]),
    ...mapActions(useCommentStore, ["fetchCommentsByUser"]),
  },
  async created() {
    this.fetchPostsByUser(this.$route.params.id);
    this.fetchCommentsByUser(this.$route.params.id);
  },
  data() {
    return {
      activeTab: "posts",
    };
  },
});
</script>
<template>
  <div class="w-full h-full min-h-screen bg-[#DAE0E6]">
    <div
      class="bg-white rounded flex flex-row justify-center gap-10 shadow p-4"
    >
      <div class="flex flex-row items-center space-x-4">
        <img
          :src="posts[0]?.author.avatarUrl"
          alt="User Avatar"
          class="w-12 h-12 rounded-full"
        />
        <h2 class="text-2xl font-bold">{{ posts[0]?.author.username }}</h2>
      </div>
      <div class="bg-white text-center">
        <button
          @click="activeTab = 'posts'"
          :class="{
            'bg-blue-500 text-white': activeTab === 'posts',
            'bg-white text-black': activeTab !== 'posts',
          }"
          class="px-4 py-2 rounded-tl rounded-tr"
        >
          Posts
        </button>
        <button
          @click="activeTab = 'comments'"
          :class="{
            'bg-blue-500 text-white': activeTab === 'comments',
            'bg-white text-black': activeTab !== 'comments',
          }"
          class="px-4 py-2 rounded-bl rounded-br"
        >
          Comments
        </button>
      </div>
    </div>
    <div class="w-screen max-w-xl mx-auto space-y-4">
      <div v-if="activeTab === 'posts'">
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
      <div v-else-if="activeTab === 'comments'">
        <CommentCard
          v-for="comment in comments"
          :key="comment.id"
          :comment="comment"
          class="bg-white rounded shadow p-4"
        />
      </div>
    </div>
  </div>
</template>
