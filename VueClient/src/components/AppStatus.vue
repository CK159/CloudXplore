<template>
	<div class="appStatus">
		Client: v{{footerVersion}} | {{footerLabel}} | Build {{buildDateFormat(footerAppBuildDate)}}<br />
		Server:
		<template v-if="applicationInfoStatus === 'loaded'">
			v{{applicationInfo.applicationVersion}}
			|
			Built {{buildDateFormat(applicationInfo.applicationBuildDate)}}
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
</template>

<script>
import dateformat from "dateformat";
import api from "../code/api";

export default {
	name: "appStatus",
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
			api.get("applicationInfo")
				.then(data => {
					this.applicationInfo = data;
					this.applicationInfoStatus = "loaded";
				})
				.catch(e => {
					this.applicationInfoStatus = "error"
				});
		},
		buildDateFormat(date) {
			return dateformat(date, "yyyy-mm-dd hh:MM TT");
		}
	},
	created() {
		this.fetchServerInfo();
	}
}
</script>

<style>
</style>
