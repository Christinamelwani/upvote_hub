<script lang="ts">
import { defineComponent } from "vue";
import { usePostStore } from "@/stores/postStore";
import { useCommentStore } from "@/stores/commentStore";
import { mapState, mapActions } from "pinia";

export default defineComponent({
  name: "PostDetail",
  computed: {
    ...mapState(usePostStore, ["activePost"]),
  },
  data() {
    return {
      voteCount: 0,
      commentText: "",
    };
  },
  methods: {
    ...mapActions(usePostStore, [
      "fetchActivePost",
      "voteOnPost",
      "getVoteCount",
    ]),
    ...mapActions(useCommentStore, ["createComment"]),
    async vote(up: boolean) {
      try {
        await this.voteOnPost(this.activePost.id, up);
        this.voteCount = await this.getVoteCount(this.activePost.id);
      } catch (error) {
        console.error("An error occurred while voting:", error);
      }
    },
    async comment() {
      try {
        this.createComment(this.activePost.id, this.commentText);
        this.commentText = "";
      } catch (err) {
        console.log(err);
      }
    },
  },
  async created() {
    try {
      this.fetchActivePost(this.$route.params.id);
      this.voteCount = await this.getVoteCount(+this.$route.params.id);
    } catch (error) {
      console.error("An error occurred while fetching the post:", error);
    }
  },
});
</script>

<template>
  <div class="flex flex-col items-center">
    <div class="bg-white rounded-lg w-4/5 p-4 m-4">
      <div class="mb-4">
        <h2 class="text-3xl font-semibold text-gray-800 mb-4">
          {{ activePost.title }}
        </h2>
        <img
          class="w-full h-auto rounded-lg"
          :src="activePost.imageUrl"
          alt="Post Image"
        />
      </div>
      <div class="mb-4 flex justify-between items-center text-blue-500">
        <a :href="activePost.link" class="hover:underline break-all text-sm">
          Link</a
        >
        <p
          @click="$router.push(`/users/${activePost.author?.id}`)"
          class="text-sm font-semibold cursor-pointer"
        >
          By: {{ activePost.author?.username || "Unknown" }}
        </p>
      </div>
      <p class="text-gray-700 text-base">{{ activePost.text }}</p>
      <div class="mt-4 flex items-center space-x-4">
        <button
          class="flex items-center space-x-2 text-green-500"
          @click="vote(true)"
        >
          <i class="fas fa-thumbs-up"></i>
          <span class="text-sm">Upvote</span>
        </button>
        <p class="text-gray-600 text-sm">{{ voteCount }}</p>
        <button
          class="flex items-center space-x-2 text-red-500"
          @click="vote(false)"
        >
          <i class="fas fa-thumbs-down"></i>
          <span class="text-sm">Downvote</span>
        </button>
      </div>
      <div class="mt-4">
        <h3 class="text-2xl font-semibold text-gray-800 mb-2">Comments</h3>
        <ul>
          <li
            v-for="comment in activePost.comments"
            :key="comment.id"
            class="mb-4 border border-gray-300 rounded-md p-4 flex items-start"
          >
            <div class="flex items-start space-x-4">
              <img
                @click="$router.push(`/users/${comment.author?.id}`)"
                :src="comment.author?.avatarUrl"
                class="w-12 h-12 rounded-full cursor-pointer"
              />
              <div>
                <p class="text-gray-800 font-semibold text-lg">
                  {{ comment.author?.username || "Unknown" }}:
                </p>
                <p class="text-gray-700 text-base">{{ comment.text }}</p>
              </div>
            </div>
          </li>
        </ul>
      </div>
      <form class="mt-4" @submit.prevent="comment">
        <textarea
          v-model="commentText"
          class="w-full rounded-md p-2 border border-gray-300"
          rows="4"
          placeholder="Add a comment..."
        ></textarea>
        <button
          @click.prevent="comment"
          class="mt-2 bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
          type="submit"
        >
          Submit Comment
        </button>
      </form>
    </div>
  </div>
</template>
