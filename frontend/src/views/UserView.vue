<script lang="ts">
import { defineComponent } from "vue";
import PostCard from "@/components/PostCard.vue";
import axios from "axios";
import CommentCard from "@/components/CommentCard.vue";

interface Post {
  id: number;
  title: string;
  imageUrl: string;
  text: string;
  link: string;
  author: {
    avatarUrl: string;
    username: string;
    id: number;
  };
  Votes: [];
}

export default defineComponent({
  components: {
    PostCard,
    CommentCard,
  },
  async created() {
    const response = await axios.get(
      `http://localhost:5066/user/${this.$route.params.id}/posts`
    );
    this.posts = response.data;
  },
  data() {
    return {
      posts: [] as Array<Post>,
      comments: [],
      activeTab: "posts",
    };
  },
});
</script>
<template>
  <div class="w-full h-full min-h-screen bg-gray-900">
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
        <PostCard
          v-for="post in posts"
          :key="post.id"
          :post="post"
          class="bg-white rounded shadow p-4"
        />
      </div>
      <div v-else-if="activeTab === 'comments'">
        <CommentCard
          v-for="comment in comments"
          :key="comment"
          :comment="comment"
          class="bg-white rounded shadow p-4"
        />
      </div>
    </div>
  </div>
</template>
