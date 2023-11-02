<script lang="ts">
import { defineComponent } from "vue";
import PostCard from "@/components/PostCard.vue";
import axios from "axios";
import VoteNookCard from "@/components/VoteNookCard.vue";

export default defineComponent({
  components: {
    VoteNookCard,
  },
  async created() {
    try {
      const response = await axios.get(`http://localhost:5066/voteNook`);
      this.voteNooks = response.data;
    } catch (err) {
      console.log(err);
    }
  },
  data() {
    return {
      voteNooks: [],
    };
  },
});
</script>
<template>
  <div class="w-full h-full min-h-screen p-4 bg-gray-900">
    <div class="w-screen max-w-xl mx-auto space-y-4">
      <VoteNookCard
        v-for="voteNook in voteNooks"
        :key="voteNook"
        :voteNook="voteNook"
        class="bg-white rounded shadow p-4"
      />
    </div>
  </div>
</template>
