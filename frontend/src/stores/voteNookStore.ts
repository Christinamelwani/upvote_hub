import { defineStore } from "pinia";
import axios from "axios";

export interface VoteNook {
  id: number;
  name: string;
  about: string;
  creator: {
    username: string;
  };
}

export const useVoteNookStore = defineStore("post", {
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
        // Send a POST request to create a new Votenook
        const response = await axios.post(
          "http://localhost:5066/voteNook",
          this.newVoteNook
        );

        // Clear the form inputs
        this.newVoteNook.name = "";
        this.newVoteNook.about = "";
      } catch (err) {
        console.log(err);
        // Handle any error or show an error message here.
      }
    },
  },
});
