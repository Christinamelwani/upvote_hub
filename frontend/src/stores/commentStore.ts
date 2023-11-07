import { defineStore } from "pinia";
import axios from "axios";
import { usePostStore } from "./postStore";
import Swal from "sweetalert2";

export interface Comment {
  id: number;
  text: string;
  author: {
    avatarUrl: string;
    username: string;
    id: number;
  };
  post: {
    id: number;
    title: string;
  };
}

export const useCommentStore = defineStore("comment", {
  state: () => ({ comments: [] as Array<Comment> }),
  getters: {},
  actions: {
    async fetchCommentsByUser(userId: string | string[]) {
      try {
        const response = await axios.get(
          `http://localhost:5066/user/${userId}/comments`
        );
        this.comments = response.data;
      } catch (err) {
        console.error(err);
      }
    },

    async createComment(postId: number, text: string) {
      const token = localStorage.getItem("access_token");
      if (!token) {
        Swal.fire("You must be logged in to comment on this post!");
        return;
      }

      if (!text) {
        Swal.fire("Can't create empty comment!");
        return;
      }

      try {
        const response = await axios.post(
          `http://localhost:5066/Comment`,
          {
            postId,
            text,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        usePostStore().fetchActivePost("" + postId);
      } catch (error) {
        console.error("An error occurred while creating the comment:", error);
      }
    },
  },
});
