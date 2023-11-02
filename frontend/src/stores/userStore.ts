import { defineStore } from "pinia";
import axios from "axios";

export interface User {
  id: number;
  username: string;
  email: string;
  avatarUrl: string;
}

export const useUserStore = defineStore("user", {
  state: () => ({ user: {} as User, loggedIn: false }),
  getters: {},
  actions: {
    async fetchUser() {
      const token = localStorage.getItem("access_token");
      if (token) {
        const response = await axios.get(`http://localhost:5066/user/`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (response.data.email) {
          this.user = response.data;
          this.loggedIn = true;
        }
      }
    },
    logout() {
      localStorage.removeItem("access_token");
      this.user = { id: 0, username: "", email: "", avatarUrl: "" };
      this.loggedIn = false;
    },
  },
});
