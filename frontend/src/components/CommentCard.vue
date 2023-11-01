<script lang="ts">
import { defineComponent } from "vue";
interface Comment {
  id: number;
  title: string;
  text: string;
  author: {
    avatarUrl: string;
    username: string;
    id: number;
  };
  Votes: [];
}

export default defineComponent({
  name: "CommentCard",
  props: {
    comment: {
      type: Object as () => Comment,
      required: true,
    },
  },
});
</script>
<template>
  <div class="bg-white rounded-lg shadow-md p-4 m-4">
    <div class="mb-4">
      <h2 class="text-2xl font-semibold text-gray-800 mb-2">
        {{ comment?.title }}
      </h2>
      <p class="text-gray-700 text-sm line-clamp-3">
        {{ comment?.text }}
      </p>
    </div>
  </div>
</template>
