<script lang="ts">
	import { goto } from '$app/navigation';
	import UserForm from '../../components/UserForm.svelte';

	async function login(email: string, password: string) {
		try {
			if (!email || !password) {
				throw new Error('Email and password are both required!');
			}

			const response = await fetch('http://localhost:5066/user/login', {
				method: 'POST', // Specify the HTTP method, assuming it's a login request
				headers: {
					'Content-Type': 'application/json' // Specify the content type of the request body
				},
				body: JSON.stringify({
					email,
					password
				})
			});

			let apiResponse = await response.json();

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
	<UserForm submitAction={login} submitText="Log in" />
</div>
