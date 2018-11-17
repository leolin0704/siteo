import Vue from 'vue/dist/vue.js';
import router from './route/index';
import Router from 'vue-router';
Vue.use(Router);
import Element from 'element-ui';
import './element-variables.scss';
Vue.use(Element, { size: 'small', zIndex: 3000 });

import lang from 'element-ui/lib/locale/lang/en';
import locale from 'element-ui/lib/locale';
locale.use(lang);


import App from './App.vue';
import "./assets/css/common.scss";

Vue.config.productionTip = false;


import * as fiters from './filters'

Object.keys(fiters).forEach(key => {
  Vue.filter(key, fiters[key])
}) 

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
