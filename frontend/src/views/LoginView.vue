<script lang="ts">
import UserForm from "@/components/UserForm.vue";
import axios from "axios";
import Swal from "sweetalert2";
import { defineComponent } from "vue";

export default defineComponent({
  components: {
    UserForm,
  },
  methods: {
    async login(email: string, password: string) {
      try {
        if (!email || !password) {
          throw new Error("Email and password are both required!");
        }

        const response = await axios.post("http://localhost:5066/user/login", {
          email,
          password,
        });

        const { token } = response.data;
        localStorage.setItem("access_token", token);

        Swal.fire("Successfully logged in!");

        this.$router.push("/");
      } catch (error: any) {
        Swal.fire(error.response.data);
      }
    },
  },
});
</script>

<template>
  <div>
    <UserForm :submitAction="login" submitText="Log In" />
  </div>
</template>
