import Vue from 'vue';
import App from './App';
import router from './router';
import store from './store';
Vue.config.productionTip = false;
new Vue({
    router: router,
    store: store,
    render: function (h) { return h(App); },
}).$mount('#app');
//# sourceMappingURL=main.js.map