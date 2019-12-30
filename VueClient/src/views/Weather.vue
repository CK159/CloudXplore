<template>
	<div class="sample-weather-container">
		<h1>Weather from the API</h1>
		
		<div class="weather-loader" v-if="loading">
			Probably Loading Something...
		</div>
		
		<table class="weather-content" v-if="!loading">
			<tr>
				<th>Date</th>
				<th>&deg;C</th>
				<th>&deg;F</th>
				<th>Summary</th>
			</tr>
			
			<tr v-for="item in weatherData">
				<td>{{item.date}}</td>
				<td>{{item.temperatureC}}</td>
				<td>{{item.temperatureF}}</td>
				<td>{{item.summary}}</td>
			</tr>
		</table>
		
		<button @click="refresh" :disabled="loading">Refresh Weather</button>
	</div>
</template>

<script>
export default {
	name: "weather",
	data: () => {
		return {
			loading: false,
			weatherData: []
		};
	},
	methods: {
		refresh() {
			this.loading = true;

			fetch(process.env.VUE_APP_API_BASE + "weatherForecast")
				.then((response) => {
					return response.json();
				})
				.then((data) => {
					this.weatherData = data;
				})
				.finally(() => {
					this.loading = false;
				});
		}
	},
	mounted() {
		this.refresh();
	}
}
</script>
