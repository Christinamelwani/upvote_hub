<script lang="ts">
import { defineComponent, PropType } from "vue";
import { Comment } from "@/stores/commentStore";

export default defineComponent({
  name: "CommentCard",
  props: {
    comment: {
      type: Object as PropType<Comment>,
      required: true,
    },
  },
});
</script>

<template>
  <div
    @click="$router.push(`/posts/${comment.post.id}`)"
    class="bg-white cursor-pointer rounded-lg shadow-md p-4 m-4"
  >
    <div class="mb-4">
      <div class="flex items-center space-x-5">
        <img :src="comment.author.avatarUrl" class="w-12 h-12 rounded-full" />
        <h2 class="text-xl text-gray-800 mb-2">
          {{ comment?.author.username }}:
        </h2>
      </div>
      <h2 class="text-xl text-gray-800 mb-2">
        {{ comment?.text }}
      </h2>
      <p class="text-gray-700 text-sm font-semibold line-clamp-3">
        {{ comment?.post.title }}
      </p>
    </div>
  </div>
</template>
