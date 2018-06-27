import Vue from 'vue';
import Vuex from 'vuex';
import SchemaModule from '@/store/SchemaModule';
Vue.use(Vuex);
var actions = {};
var getters = {};
var mutations = {};
var store = new Vuex.Store({
    state: {
        schemaModule: {
            tables: new Array(),
        },
    },
    actions: actions,
    mutations: mutations,
    getters: getters,
    modules: {
        SchemaModule: SchemaModule,
    },
});
export default store;
//# sourceMappingURL=index.js.map