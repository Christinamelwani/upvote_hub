import { defineStore } from "pinia";
import axios from "axios";

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
}

export const usePostStore = defineStore("post", {
  state: () => ({ posts: [] as Array<Post> }),
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
  },
});
