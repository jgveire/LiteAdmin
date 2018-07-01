import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home';
import Table from './views/Table';
import Edit from '@/views/Edit';

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home,
        },
        {
            path: '/tables/:tableName/edit/:id',
            name: 'edit',
            component: Edit,
        },
        {
            path: '/tables/:tableName',
            name: 'table',
            component: Table,
        },
    ],
});
