import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home';
import Table from './views/Table';
Vue.use(Router);
export default new Router({
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home,
        },
        {
            path: '/tables/:tableName',
            name: 'table',
            component: Table,
        },
    ],
});
//# sourceMappingURL=router.js.map