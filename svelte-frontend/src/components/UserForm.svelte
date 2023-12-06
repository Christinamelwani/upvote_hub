<script lang="ts">
	export let submitAction;

	export let submitText: string = '';

	let email: string;
	let password: string;
	let avatarUrl: string;
	let username: string;
</script>

<div class="flex flex-row">
	<img src="/bbb584033aa89e39bad69436c504c9bd.png" />
	<section class="bg-white w-full">
		<div class="flex flex-col justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
			<div class="w-full bg-white rounded-lg md:mt-0 sm:max-w-md xl:p-0">
				<div class="p-6 space-y-4 md:space-y-6 sm:p-8">
					<h1 class="text-[18px] font-[500] leading-tight tracking-tight text-gray-900 md:text-2xl">
						{submitText}
					</h1>
					<form class="space-y-4 md:space-y-6">
						<div>
							<input
								bind:value={email}
								type="email"
								name="email"
								id="email"
								style="padding: 12px 10px"
								class="border border-[1px] border-[rgba(0,0,0,.1)] rounded-lg bg-[#fcfcfb] block w-full placeholder-[#a5a4a4]"
								placeholder="Email"
							/>
						</div>
						{#if submitText == 'Register'}
							<div>
								<input
									bind:value={username}
									type="text"
									name="username"
									id="username"
									style="padding: 12px 10px"
									class="border border-[1px] border-[rgba(0,0,0,.1)] rounded-lg bg-[#fcfcfb] block w-full p-2.5 placeholder-[#a5a4a4]"
									placeholder="Username"
								/>
							</div>
							<div>
								<input
									bind:value={avatarUrl}
									type="text"
									name="avatar_url"
									id="avatar_url"
									style="padding: 12px 10px"
									class="border border-[1px] border-[rgba(0,0,0,.1)] rounded-lg bg-[#fcfcfb] block w-full p-2.5 placeholder-[#a5a4a4]"
									placeholder="Avatar Link"
								/>
							</div>
						{/if}
						<div>
							<input
								bind:value={password}
								type="password"
								name="password"
								id="password"
								style="padding: 12px 10px"
								placeholder="Password"
								class="border border-[1px] border-[rgba(0,0,0,.1)] rounded-lg bg-[#fcfcfb] block w-full p-2.5 placeholder-[#a5a4a4]"
							/>
						</div>
						{#if submitText == 'Register'}
							<div>
								Already have an account?
								<a href="/login" class="text-blue-500">Login</a>
							</div>
						{:else}
							<div>
								No account yet?
								<a class="text-blue-500" href="/register">Register</a>
							</div>
						{/if}
						<button
							on:click={(e) => {
								e.preventDefault();
								submitAction(email, password, username, avatarUrl);
							}}
							style="padding: 12px 10px"
							class="w-full text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800"
						>
							{submitText}
						</button>
					</form>
				</div>
			</div>
		</div>
	</section>
</div>
