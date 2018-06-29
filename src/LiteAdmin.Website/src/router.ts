import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home';
import Table from './views/Table';
import Details from './views/Details';

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home,
        },
        {
            path: '/tables/:tableName/details/:id',
            name: 'details',
            component: Details,
        },
        {
            path: '/tables/:tableName',
            name: 'table',
            component: Table,
        },
    ],
});
