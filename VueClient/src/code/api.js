export default {
	get,
	post
}

function get(path) {
	const url = process.env.VUE_APP_API_BASE + path;
	const config = {
		headers: {
			"Content-Type": "application/json"
		},
	};

	return fetch(url, config)
		.then(throwOnFetchErrors)
		.then(convertToJson)
		.catch(e => logAndRethrow(e, url));
}

function post(path, data) {
	const url = process.env.VUE_APP_API_BASE + path;
	const config = {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify(data)
	};

	return fetch(url, config)
		.then(throwOnFetchErrors)
		.then(convertToJson)
		.catch(e => logAndRethrow(e, url));
}

function throwOnFetchErrors(response) {
	if (!response.ok) {
		throw Error(response.statusText);
	}
	return response;
}

function convertToJson(data) {
	return data.json();
}

function logAndRethrow(e, url) {
	console.error(e, url);
	throw e;
}
