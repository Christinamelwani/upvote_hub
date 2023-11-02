<script lang="ts">
import { Vote, usePostStore } from "@/stores/postStore";
import { useUserStore } from "@/stores/userStore";
import { defineComponent, PropType } from "vue";

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
    user() {
      return useUserStore().user;
    },
  },
  methods: {
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
      if (this.post.id) {
        await usePostStore().voteOnPost(this.post.id, up);
        this.voteCount = this.calculateVoteCount();
      }
    },
    calculateVoteCount() {
      const upvotes = (this.post.votes || []).filter(
        (vote: Vote) => vote.up
      ).length;
      const downvotes = (this.post.votes || []).filter(
        (vote: Vote) => !vote.up
      ).length;
      return upvotes - downvotes;
    },
  },
  mounted() {
    this.voteCount = this.calculateVoteCount();
  },
});
</script>

<template>
  <div class="bg-white rounded-lg shadow-md p-4 m-4">
    <div class="mb-4">
      <h2
        @click="goToPost"
        class="text-2xl cursor-pointer font-semibold text-gray-800 mb-2"
      >
        {{ post.title }}
      </h2>
      <img
        @click="goToPost"
        class="w-full cursor-pointer h-auto rounded-md"
        :src="post.imageUrl"
        alt="Post Image"
      />
    </div>
    <div class="mb-2 flex flex-row justify-between items-center text-blue-500">
      <a :href="post.link" class="hover:underline break-all">{{ post.link }}</a>
      <p @click="goToAuthor" class="text-sm font-semibold cursor-pointer">
        {{ post.author.username }}
      </p>
    </div>
    <p class="text-gray-700 text-sm line-clamp-3">{{ post.text }}</p>
    <div class="mt-4 flex items-center justify-between w-[50%]">
      <button
        @click="vote(true)"
        class="flex items-center space-x-2 text-green-500"
      >
        <i class="fas fa-thumbs-up"></i>
        <span>Upvote</span>
      </button>
      <p class="text-gray-600">{{ voteCount }}</p>
      <button
        @click="vote(false)"
        class="flex items-center space-x-2 text-red-500 ml-4"
      >
        <i class="fas fa-thumbs-down"></i>
        <span>Downvote</span>
      </button>
    </div>
  </div>
</template>
