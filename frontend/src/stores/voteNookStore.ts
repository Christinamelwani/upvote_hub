import { defineStore } from "pinia";
import axios, { AxiosError } from "axios";
import Swal from "sweetalert2";

export interface VoteNook {
  id: number;
  name: string;
  about: string;
  creator: {
    username: string;
  };
}

interface axiosError {
  response: {
    data: string;
  };
}

export const useVoteNookStore = defineStore("voteNook", {
  state: () => ({
    voteNooks: [] as Array<VoteNook>,
    newVoteNook: {} as VoteNook,
    activeVoteNook: {} as VoteNook,
  }),
  getters: {},
  actions: {
    async fetchVoteNooks() {
      try {
        const response = await axios.get(`http://localhost:5066/voteNook`);
        this.voteNooks = response.data;
      } catch (err) {
        console.log(err);
      }
    },
    async createVoteNook() {
      try {
        const token = localStorage.getItem("access_token");
        if (!token) {
          Swal.fire("You must be logged in to create a votenook!");
        }

        const response = await axios.post(
          "http://localhost:5066/voteNook",
          this.newVoteNook,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        this.newVoteNook.name = "";
        this.newVoteNook.about = "";

        this.fetchVoteNooks();
      } catch (err) {
        if (err instanceof AxiosError && err.response) {
          Swal.fire(err.response.data);
        } else {
          console.error("An unexpected error occurred:", err);
        }
      }
    },
  },
});
