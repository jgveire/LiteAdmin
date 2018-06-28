import Vue from 'vue';
import App from './App';
import router from './router';
import store from './store';
import Navigation from '@/components/Navigation';

Vue.config.productionTip = false;

Vue.component('Navigation', Navigation);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
