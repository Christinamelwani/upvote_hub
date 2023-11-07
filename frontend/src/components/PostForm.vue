<script lang="ts">
import { useVoteNookStore } from "@/stores/voteNookStore";
import { mapState, mapActions } from "pinia";
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    submitAction: { type: Function, required: true },
    submitText: { type: String, required: true },
  },
  computed: {
    ...mapState(useVoteNookStore, ["voteNooks"]),
  },
  methods: {
    ...mapActions(useVoteNookStore, ["fetchVoteNooks"]),
  },
  created() {
    this.fetchVoteNooks();
  },
  data() {
    return {
      post: {
        title: "",
        imageUrl: "",
        link: "",
        text: "",
        voteNookId: null,
      },
    };
  },
});
</script>
<template>
  <div class="flex items-center justify-center min-h-screen bg-[#DAE0E6]">
    <section class="bg-white w-full md:max-w-md xl:p-0 rounded-lg">
      <div class="p-6 space-y-4 sm:p-8">
        <h1 class="text-2xl font-semibold text-gray-900">{{ submitText }}</h1>
        <form class="space-y-4">
          <div>
            <input
              v-model="post.title"
              type="text"
              name="title"
              id="title"
              class="w-full px-3 py-2.5 rounded-lg border border-gray-300 focus:outline-none"
              placeholder="Title"
            />
          </div>
          <div>
            <input
              v-model="post.imageUrl"
              type="text"
              name="imageUrl"
              id="imageUrl"
              class="w-full px-3 py-2.5 rounded-lg border border-gray-300 focus:outline-none"
              placeholder="Image URL"
            />
          </div>
          <div>
            <input
              v-model="post.link"
              type="text"
              name="link"
              id="link"
              class="w-full px-3 py-2.5 rounded-lg border border-gray-300 focus:outline-none"
              placeholder="Link"
            />
          </div>
          <div>
            <input
              v-model="post.text"
              type="text"
              name="text"
              id="text"
              class="w-full px-3 py-2.5 rounded-lg border border-gray-300 focus:outline-none"
              placeholder="Text"
            />
          </div>
          <div>
            <label for="voteNookId" class="text-gray-800 font-medium"
              >Vote Nook:</label
            >
            <select
              v-model="post.voteNookId"
              name="voteNookId"
              id="voteNookId"
              class="w-full px-3 py-2.5 rounded-lg border border-gray-300 focus:outline-none"
            >
              <option value="" disabled>Select a Vote Nook</option>
              <option v-for="nook in voteNooks" :key="nook.id" :value="nook.id">
                {{ nook.name }}
              </option>
            </select>
          </div>
          <button
            @click.prevent="submitAction(post)"
            class="w-full px-5 py-2.5 text-white bg-blue-600 hover:bg-blue-700 rounded-lg font-medium text-sm focus:ring-4 focus:outline-none focus:ring-primary-300"
          >
            {{ submitText }}
          </button>
        </form>
      </div>
    </section>
  </div>
</template>
