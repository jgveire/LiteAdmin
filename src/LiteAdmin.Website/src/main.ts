import Vue from 'vue';
import App from './App';
import router from './router';
import store from './store';
import Navigation from '@/components/Navigation';
import { MdButton } from 'vue-material/dist/components';
import 'vue-material/dist/vue-material.min.css';

Vue.config.productionTip = false;

Vue.component('Navigation', Navigation);
Vue.use(MdButton);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
