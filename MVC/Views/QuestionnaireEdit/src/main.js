import Vue from 'vue'
import App from './Questionnaire.vue'
import vuetify from './plugins/vuetify'

Vue.config.productionTip = false
window.globalEvent = new Vue();


new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
