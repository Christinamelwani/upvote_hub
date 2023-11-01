<script lang="ts">
import { defineComponent } from "vue";

interface VoteNook {
  name: string;
  about: string;
  creator: {
    username: string;
  };
}

export default defineComponent({
  name: "VoteNookCard",
  props: {
    voteNook: {
      type: Object as () => VoteNook,
      required: true,
    },
  },
});
</script>
<template>
  <div class="bg-white rounded-lg shadow-md p-4 m-4">
    <div class="mb-4">
      <h2 class="text-2xl font-semibold text-gray-800 mb-2">
        {{ voteNook?.name }}
      </h2>
    </div>
    <p class="text-gray-700 text-sm line-clamp-3">{{ voteNook?.about }}</p>
    <p class="text-gray-700 text-sm line-clamp-3">
      Created by: {{ voteNook?.creator.username }}
    </p>
  </div>
</template>
