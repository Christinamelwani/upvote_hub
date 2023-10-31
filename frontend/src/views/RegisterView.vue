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
    async register(
      email: string,
      password: string,
      username: string,
      avatarUrl: string
    ) {
      try {
        if (!email || !password || !username || !avatarUrl) {
          throw new Error(
            "Email, username, avatar url and password are all required!"
          );
        }

        const response = await axios.post(
          "http://localhost:5066/user/register",
          {
            email,
            password,
            username,
            avatarUrl,
          }
        );

        Swal.fire("Successfully Registered!");

        const loginResponse = await axios.post(
          "http://localhost:5066/user/login",
          {
            email,
            password,
          }
        );

        const { token } = loginResponse.data;
        localStorage.setItem("access_token", token);

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
    <UserForm :submitAction="register" submitText="Register" />
  </div>
</template>
