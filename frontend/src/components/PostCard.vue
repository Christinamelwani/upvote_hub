<script lang="ts">
import { defineComponent } from "vue";

interface Post {
  title: string;
  imageUrl: string;
  text: string;
  link: string;
}

export default defineComponent({
  name: "PostCard",
  props: {
    post: {
      type: Object as () => Post,
      required: true,
    },
  },
});
</script>
<template>
  <div class="bg-white rounded-lg shadow-md p-4 m-4">
    <div class="mb-4">
      <h2 class="text-2xl font-semibold text-gray-800 mb-2">
        {{ post?.title }}
      </h2>
      <img
        class="w-full h-auto rounded-md"
        :src="post?.imageUrl"
        alt="Post Image"
      />
    </div>
    <div class="mb-2">
      <a :href="post.link" class="text-blue-500 hover:underline break-all">{{
        post.link
      }}</a>
    </div>
    <p class="text-gray-700 text-sm line-clamp-3">
      {{ post?.text }}
    </p>
  </div>
</template>
