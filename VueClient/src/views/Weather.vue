<template>
	<div class="sample-weather-container">
		<h1>Weather from the API</h1>
		
		<div class="weather-loader" :class="{'invisible': !loading}">
			Probably Loading Something...
		</div>
		
		<div class="weather-content-container">
			<table class="weather-content" :class="{'invisible': loading}">
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
		</div>
		
		<button @click="refresh" :disabled="loading">Refresh Weather</button>
	</div>
</template>

<script>
	import api from "../code/api";
	
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
				api.get("weatherForecast")
					.then(data => this.weatherData = data)
					.finally(() => this.loading = false);
			}
		},
		mounted() {
			this.refresh();
		}
	}
</script>

<style scoped>
	.invisible {
		visibility: hidden;
	}
	.weather-content-container {
		display: flex;
		justify-content: center;
		margin-bottom: 10px;
	}
</style>
