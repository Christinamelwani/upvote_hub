import { defineStore } from "pinia";
import axios from "axios";
import { Comment } from "@/stores/commentStore";
export interface Post {
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
  comments: Array<Comment>;
}

export const usePostStore = defineStore("post", {
  state: () => ({ posts: [] as Array<Post>, activePost: {} as Post }),
  getters: {},
  actions: {
    async fetchPosts(query: string) {
      try {
        let response;
        if (query) {
          response = await axios.get(
            `http://localhost:5066/post/search?query=${query}`
          );
        } else {
          response = await axios.get("http://localhost:5066/post");
        }

        this.posts = response.data;
      } catch (err) {
        console.log(err);
      }
    },

    async fetchPostsByUser(userId: string | string[]) {
      try {
        const response = await axios.get(
          `http://localhost:5066/user/${userId}/posts`
        );
        this.posts = response.data;
      } catch (err) {
        console.log(err);
      }
    },

    async fetchActivePost(postId: string | string[]) {
      try {
        const response = await axios.get(
          `http://localhost:5066/post/${postId}`
        );
        this.activePost = response.data;
      } catch (err) {
        console.log(err);
      }
    },

    async fetchPostsInVoteNook(voteNookId: string | string[]) {
      try {
        const response = await axios.get(
          `http://localhost:5066/votenook/${voteNookId}/posts`
        );
        this.posts = response.data;
      } catch (err) {
        console.log(err);
      }
    },
  },
});
