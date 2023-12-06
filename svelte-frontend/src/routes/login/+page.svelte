<script lang="ts">
	import { goto } from '$app/navigation';
	import UserForm from '../../components/UserForm.svelte';
	import swal from 'sweetalert2';

	async function login(email: string, password: string) {
		try {
			if (!email || !password) {
				throw new Error('Email and password are both required!');
			}

			const response = await fetch('http://localhost:5066/user/login', {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ email, password })
			});

			if (response.ok) {
				const { token } = await response.json();
				localStorage.setItem('access_token', token);
				swal.fire('Successfully logged in!');
				goto('/');
			} else {
				const errorText = await response.text();
				throw new Error(errorText);
			}
		} catch (error: any) {
			const errorMessage =
				error.response?.data || error.message || 'An unexpected error occurred during login.';
			swal.fire({
				icon: 'error',
				title: 'Login Error',
				text: errorMessage
			});
		}
	}
</script>

<div>
	<UserForm submitAction={login} submitText="Log in" />
</div>
