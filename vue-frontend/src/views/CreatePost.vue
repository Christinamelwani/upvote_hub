<script lang="ts">
import PostForm from "@/components/PostForm.vue";
import { defineComponent } from "vue";
import { mapState, mapActions } from "pinia";
import { usePostStore } from "@/stores/postStore";

export default defineComponent({
  components: {
    PostForm,
  },
  methods: {
    ...mapActions(usePostStore, ["createPost"]),
  },
});
</script>

<template>
  <div>
    <PostForm :submitAction="createPost" submitText="Create post" />
  </div>
</template>
