const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: [
    'vuetify'
  ],
indexPath: "tmp.html",
	outputDir:'../../wwwroot/Scripts/vue-apps/questionnaire',
	filenameHashing: false,
	runtimeCompiler: true
})
