<template>
	<div id="app">
		<div id="nav">
			<router-link to="/">Home</router-link>
			| <router-link to="/about">About</router-link>
			| <router-link to="/weather">Weather</router-link>
		</div>
		
		<router-view/>
		
		<div id="footer">
			Client: v{{footerVersion}} | {{footerLabel}} | Build {{footerAppBuildDate}}<br />
			Server: 
			<template v-if="applicationInfoStatus === 'loaded'">
				v{{applicationInfo.applicationVersion}}
				|
				Built {{applicationInfo.applicationBuildDate}}
				|
				{{footerApiBase}}
				|
				<span title="Managed memory allocated by GC">{{applicationInfo.memory.gcAllocatedMemoryMb}}MB</span>
				/ <span title="Process working set memory">{{applicationInfo.memory.processWorkingSetMemoryMb}}MB</span>
			</template>
			<template v-else>
				<!--Always display the intended API URL even if it can't be contacted-->
				{{footerApiBase}}
			</template>
			|
			<a href="#" @click.prevent="fetchServerInfo">Refresh</a>
		</div>
	</div>
</template>

<script>
export default {
	name: 'app',
	data: () => {
		return {
			footerLabel: process.env.VUE_APP_CONFIG_LABEL,
			footerVersion: process.env.VUE_APP_VERSION,
			footerAppBuildDate: process.env.VUE_APP_BUILD_DATE,
			footerApiBase: process.env.VUE_APP_API_BASE,
			applicationInfoStatus: "",
			applicationInfo: {}
		};
	},
	methods: {
		fetchServerInfo() {
			if (this.applicationInfoStatus === "loading") {
				return;
			}
			
			this.applicationInfoStatus = "loading";
			
			fetch(process.env.VUE_APP_API_BASE + "applicationInfo")
				.then((response) => {
					return response.json();
				})
				.then((data) => {
					this.applicationInfo = data;
				})
				.finally(() => {
					this.applicationInfoStatus = "loaded";
				});
		}
	},
	created() {
		this.fetchServerInfo();
	}
}
</script>

<style>
	#app {
		font-family: 'Avenir', Helvetica, Arial, sans-serif;
		-webkit-font-smoothing: antialiased;
		-moz-osx-font-smoothing: grayscale;
		text-align: center;
		color: #2c3e50;
	}
	
	#nav {
		padding: 30px;
	}
	
	#nav a {
		font-weight: bold;
		color: #2c3e50;
	}
	
	#nav a.router-link-exact-active {
		color: #42b983;
	}
	
	#footer {
		text-align: right;
		color: #777;
	}
</style>
