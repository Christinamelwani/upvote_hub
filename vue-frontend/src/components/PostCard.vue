<script lang="ts">
import { usePostStore } from "@/stores/postStore";
import { defineComponent } from "vue";
import { mapState, mapActions } from "pinia";
import { useUserStore } from "@/stores/userStore";

export default defineComponent({
  name: "PostCard",
  props: {
    post: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      voteCount: 0,
    };
  },
  computed: {
    ...mapState(useUserStore, ["user"]),
  },
  methods: {
    ...mapActions(usePostStore, ["voteOnPost", "getVoteCount", "removePost"]),
    goToPost() {
      if (this.post.id) {
        this.$router.push(`/posts/${this.post.id}`);
      }
    },
    goToAuthor() {
      if (this.post.author.id) {
        this.$router.push(`/users/${this.post.author.id}`);
      }
    },
    async vote(up: boolean) {
      await this.voteOnPost(this.post.id, up);
      this.voteCount = await this.getVoteCount(this.post.id);
    },
  },
  async created() {
    this.voteCount = await this.getVoteCount(this.post.id);
  },
});
</script>

<template>
  <div class="bg-white w-[50vw] flex flex-row rounded-lg shadow-md m-4">
    <div
      class="flex bg-gray-100 flex-col items-center py-4 px-2 rounded-lg space-y-2"
    >
      <svg
        @click="vote(true)"
        class="rotate-180 w-8 cursor-pointer"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 384 512"
      >
        <path
          d="M169.4 470.6c12.5 12.5 32.8 12.5 45.3 0l160-160c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L224 370.8 224 64c0-17.7-14.3-32-32-32s-32 14.3-32 32l0 306.7L54.6 265.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3l160 160z"
        />
      </svg>
      <p class="text-gray-600">{{ voteCount }}</p>
      <svg
        @click="vote(false)"
        class="w-8 cursor-pointer"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 384 512"
      >
        <path
          d="M169.4 470.6c12.5 12.5 32.8 12.5 45.3 0l160-160c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L224 370.8 224 64c0-17.7-14.3-32-32-32s-32 14.3-32 32l0 306.7L54.6 265.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3l160 160z"
        />
      </svg>
    </div>
    <div class="mx-4 w-full py-4">
      <div class="mb-4 cursor-pointer" @click="goToPost">
        <h2 class="text-2xl font-semibold text-gray-800 mb-2">
          {{ post.title }}
        </h2>
        <img
          :src="post.imageUrl"
          alt="Post Image"
          class="w-full h-auto rounded-md"
        />
      </div>
      <div
        class="mb-2 flex flex-row justify-between items-center text-blue-500"
      >
        <a :href="post.link" class="hover:underline max-w-[50%] break-all"
          >Link</a
        >
        <p @click="goToAuthor" class="text-sm font-semibold cursor-pointer">
          {{ post.author.username }}
        </p>
        <i
          v-if="post.author.username === user.username"
          @click="removePost(post.id)"
          class="fas fa-trash-alt text-red-500 cursor-pointer"
          >Remove Post</i
        >
      </div>
      <p class="text-gray-700 text-sm line-clamp-3">{{ post.text }}</p>
    </div>
  </div>
</template>
