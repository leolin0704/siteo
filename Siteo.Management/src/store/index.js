import Vue from 'vue/dist/vue.js';
import Vuex from 'vuex';
Vue.use(Vuex);
import login from './modules/login';
import createLogger from '../plugins/logger';

const debug = process.env.NODE_ENV !== 'production';

export default new Vuex.Store({
  modules: {
    login
  },
  strict: debug,
  plugins: debug ? [createLogger()] : []
})
