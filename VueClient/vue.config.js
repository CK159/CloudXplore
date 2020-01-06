const path = require("path");

// Make the package.json version and build time available in the environmental variables which will be baked into the app at build time
process.env.VUE_APP_VERSION = require('./package.json').version;
process.env.VUE_APP_BUILD_DATE = new Date().toISOString();

module.exports = {
	outputDir: path.resolve(__dirname, "../Api/wwwroot")
};
