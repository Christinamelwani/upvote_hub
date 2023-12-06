<script lang="ts">
	import { goto } from '$app/navigation';
	import UserForm from '../../components/UserForm.svelte';

	async function register(email: string, password: string, username: string, avatarUrl: string) {
		try {
			if (!email || !password || !username || !avatarUrl) {
				throw new Error('Email, username, avatar url and password are all required!');
			}

			const url = 'http://localhost:5066/user/register';

			const response = await fetch(url, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({
					email,
					password,
					username,
					avatarUrl
				})
			});

			console.log('Successfully Registered!');

			let loginResponse = await fetch('http://localhost:5066/user/login', {
				method: 'POST', // Specify the HTTP method, assuming it's a login request
				headers: {
					'Content-Type': 'application/json' // Specify the content type of the request body
				},
				body: JSON.stringify({
					email,
					password
				})
			});

			let apiResponse = await loginResponse.json();

			if (response.ok) {
				const { token } = apiResponse;
				localStorage.setItem('access_token', token);

				console.log('Successfully logged in!');

				goto('/');
			}
		} catch (error: any) {
			if (error.response) {
				console.log(error.response.data);
			} else {
				console.log(error.message);
			}
		}
	}
</script>

<div>
	<UserForm submitAction={register} submitText="Register" />
</div>
