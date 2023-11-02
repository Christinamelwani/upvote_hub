import { defineStore } from "pinia";
import axios from "axios";

export interface Comment {
  id: number;
  text: string;
  author: {
    avatarUrl: string;
    username: string;
    id: number;
  };
  post: {
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
  },
});
