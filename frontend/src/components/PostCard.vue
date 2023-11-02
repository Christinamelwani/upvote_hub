<script lang="ts">
import { defineComponent, PropType } from "vue";
import { Post } from "@/stores/postStore";

export default defineComponent({
  name: "PostCard",
  props: {
    post: {
      type: Object as PropType<Post>,
      required: true,
    },
  },
});
</script>
<template>
  <div class="bg-white rounded-lg shadow-md p-4 m-4">
    <div class="mb-4">
      <h2
        @click="$router.push(`/posts/${post.id}`)"
        class="text-2xl cursor-pointer font-semibold text-gray-800 mb-2"
      >
        {{ post.title }}
      </h2>
      <img
        @click="$router.push(`/posts/${post.id}`)"
        class="w-full cursor-pointer h-auto rounded-md"
        :src="post.imageUrl"
        alt="Post Image"
      />
    </div>
    <div class="mb-2 flex flex-row justify-between items-center text-blue-500">
      <a :href="post.link" class="hover:underline break-all">{{ post.link }}</a>
      <p
        @click="$router.push(`/users/${post.author.id}`)"
        class="text-sm font-semibold cursor-pointer"
      >
        {{ post.author.username }}
      </p>
    </div>
    <p class="text-gray-700 text-sm line-clamp-3">{{ post.text }}</p>
    <div class="mt-4 flex items-center">
      <button class="flex items-center space-x-2 text-green-500">
        <i class="fas fa-thumbs-up"></i>
        <span>Upvote</span>
      </button>
      <p class="text-gray-600">{{ post.Votes?.length }}</p>
      <button class="flex items-center space-x-2 text-red-500 ml-4">
        <i class="fas fa-thumbs-down"></i>
        <span>Downvote</span>
      </button>
    </div>
  </div>
</template>
