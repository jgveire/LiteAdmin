import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home';
import Overview from '@/views/Overview';
import Edit from '@/views/Edit';
import Add from '@/views/Add';

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home,
        },
        {
            path: '/maintain/:tableName/edit/:id',
            name: 'edit',
            component: Edit,
        },
        {
            path: '/maintain/:tableName/add',
            name: 'add',
            component: Add,
        },
        {
            path: '/maintain/:tableName',
            name: 'maintain',
            component: Overview,
        },
    ],
});
