module.exports = {
  transpileDependencies: [
    'vuetify'
	],
	//	pages: {
	//	index: {
	//		// точка входа для страницы
	//		entry: 'main.js',
	//		// исходный шаблон
	//		template: 'index.cshtml',
	//		// в результате будет dist/index.html
	//		filename: 'indexY.html',
	//		// когда используется опция title, то <title> в шаблоне
	//		// должен быть <title><%= htmlWebpackPlugin.options.title %></title>
	//		//title: 'Index Page',
	//		// все фрагменты, добавляемые на этой странице, по умолчанию
	//		// это извлечённые общий фрагмент и вендорный фрагмент.
	//		//chunks: ['chunk-vendors', 'chunk-common', 'index']
	//	},
	//	// когда используется строковый формат точки входа, то
	//	// шаблон будет определяться как `public/subpage.html`,
	//	// а если таковой не будет найден, то `public/index.html`.
	//	// Выходное имя файла будет определено как `subpage.html`.
	//	//subpage: 'src/subpage/main.js'
	//},

	indexPath: "tmp.html",
	outputDir:'../../Scripts/vue-apps/questionnaire/',
	filenameHashing: false,
	runtimeCompiler: true
	
}
