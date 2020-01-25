<template>
	<div id="app">
		<div id="nav">
			<router-link to="/">Home</router-link>
			| <router-link to="/about">About</router-link>
			| <router-link to="/weather">Weather</router-link>
		</div>
		
		<router-view/>
		
		<div id="footer">
			<app-status />
		</div>
	</div>
</template>

<script>
import AppStatus from "./components/AppStatus";
	
export default {
	name: "app",
	components: {
		AppStatus
	},
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
					this.applicationInfoStatus = "loaded";
				})
				.finally(() => {
				});
		}
	},
	created() {
		this.fetchServerInfo();
	}
}
</script>

<style>
	html, body {
		box-sizing: border-box;
		height: 100%;
		margin: 0;
	}
	
	#app {
		font-family: 'Avenir', Helvetica, Arial, sans-serif;
		text-align: center;
		color: #2c3e50;
		
		/*	Sticky footer */
		min-height: 100%;
		display: grid;
		grid-template-rows: auto 1fr auto; /* 3-column layout - if adding more child elements to #app then add more 'auto's */
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
		padding: 10px 15px;
	}
</style>
