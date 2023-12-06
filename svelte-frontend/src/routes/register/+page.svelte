<script lang="ts">
	import { goto } from '$app/navigation';
	import UserForm from '../../components/UserForm.svelte';
	import swal from 'sweetalert2';

	async function register(email: string, password: string, username: string, avatarUrl: string) {
		try {
			if (!email || !password || !username || !avatarUrl) {
				throw new Error('Email, username, avatar url, and password are all required!');
			}

			const registerUrl = 'http://localhost:5066/user/register';

			const registerResponse = await fetch(registerUrl, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ email, password, username, avatarUrl })
			});

			if (!registerResponse.ok) {
				const errorText = await registerResponse.text();
				throw new Error(errorText);
			}

			console.log('Successfully Registered!');

			const loginUrl = 'http://localhost:5066/user/login';
			const loginResponse = await fetch(loginUrl, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ email, password })
			});

			const apiResponse = await loginResponse.json();

			if (loginResponse.ok) {
				const { token } = apiResponse;
				localStorage.setItem('access_token', token);
				console.log('Successfully logged in!');
				goto('/');
			}
		} catch (error: any) {
			const errorMessage = error.response?.data || error.message || 'An unexpected error occurred.';
			swal.fire({
				icon: 'error',
				title: 'Registration Error',
				text: errorMessage
			});
		}
	}
</script>

<div>
	<UserForm submitAction={register} submitText="Register" />
</div>
