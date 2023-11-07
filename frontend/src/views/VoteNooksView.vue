<script lang="ts">
import { defineComponent } from "vue";
import { mapState, mapWritableState, mapActions } from "pinia";
import VoteNookCard from "@/components/VoteNookCard.vue";
import { useVoteNookStore } from "../stores/voteNookStore";

export default defineComponent({
  components: {
    VoteNookCard,
  },
  created() {
    this.fetchVoteNooks();
  },
  data() {
    return {
      showDropdown: false,
    };
  },
  computed: {
    ...mapState(useVoteNookStore, ["voteNooks"]),
    ...mapWritableState(useVoteNookStore, ["newVoteNook"]),
  },
  methods: {
    ...mapActions(useVoteNookStore, ["fetchVoteNooks", "createVoteNook"]),
    toggleDropdown() {
      this.showDropdown = !this.showDropdown;
    },
  },
});
</script>
<template>
  <div class="w-full h-full min-h-screen p-4 bg-gray-900">
    <div class="w-screen max-w-xl mx-auto space-y-4">
      <VoteNookCard
        v-for="voteNook in voteNooks"
        :key="voteNook.id"
        :voteNook="voteNook"
        class="bg-white rounded shadow p-4"
      />
    </div>
    <div class="w-screen max-w-xl mx-auto space-y-4">
      <div class="p-4">
        <div class="mb-4 flex items-center">
          <button
            @click="toggleDropdown"
            class="px-4 py-2 bg-blue-500 text-white rounded-md"
          >
            {{ showDropdown ? "Cancel" : "Create New Votenook" }}
          </button>
          <svg
            v-if="showDropdown"
            class="w-6 h-6 ml-2 cursor-pointer"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            @click="toggleDropdown"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M13 5l7 7-7 7M5 5l7 7-7 7"
            />
          </svg>
        </div>
        <div
          v-if="showDropdown"
          key="create-votenook-form"
          class="bg-white p-4 rounded-md shadow"
        >
          <form @submit.prevent="createVoteNook">
            <div class="mb-4">
              <label for="voteNookName" class="text-gray-700 font-medium">
                Name:
              </label>
              <input
                v-model="newVoteNook.name"
                type="text"
                id="voteNookName"
                name="voteNookName"
                class="mt-1 p-2 block w-full border rounded-md"
              />
            </div>
            <div class="mb-4">
              <label
                for="voteNookDescription"
                class="text-gray-700 font-medium"
              >
                Description:
              </label>
              <textarea
                v-model="newVoteNook.about"
                id="voteNookDescription"
                name="voteNookDescription"
                class="mt-1 p-2 block w-full border rounded-md"
              ></textarea>
            </div>
            <div>
              <button
                type="submit"
                class="px-4 py-2 bg-blue-500 text-white rounded-md"
              >
                Create
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>
