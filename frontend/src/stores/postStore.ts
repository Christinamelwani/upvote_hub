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
  votes: [];
  comments: Array<Comment>;
}

export interface Vote {
  id: number;
  up: boolean;
  user: {
    avatarUrl: string;
    username: string;
    id: number;
  };
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

    async voteOnPost(postId: number, up: boolean) {
      try {
        const token = localStorage.getItem("access_token");
        if (!token) {
          console.log("You must be logged in to upvote this post!");
        }

        const response = await axios.post(
          `http://localhost:5066/Vote`,
          {
            postId,
            up: up,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
      } catch (err) {
        console.log(err);
      }
    },

    async getVoteCount(postId: number) {
      const response = await axios.get(
        `http://localhost:5066/Post/${postId}/votes`
      );
      console.log(response);

      return response.data;
    },
  },
});
