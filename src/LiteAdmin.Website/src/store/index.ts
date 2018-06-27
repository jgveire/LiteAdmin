import Vue from 'vue';
import Vuex from 'vuex';
import { Store } from 'vuex';
import { MutationTree } from 'vuex';
import { ActionTree } from 'vuex';
import { GetterTree } from 'vuex';
import SchemaModule from '@/store/SchemaModule';
import { ISchemaState } from '@/store/SchemaModule';
import { ISchemaGetters } from '@/store/SchemaModule';

Vue.use(Vuex);

export interface IStore<T> extends Store<T>
{
    getters: IStoreGetters;
}

export interface IStoreState {
    schemaModule: ISchemaState;
}

// tslint:disable no-empty-interface
export interface IStoreGetters extends ISchemaGetters {
}

const actions: ActionTree<IStoreState, IStoreState> = {
};

const getters: GetterTree<IStoreState, IStoreState> = {
};

const mutations: MutationTree<IStoreState> = {
};

const store: IStore<IStoreState> = new Vuex.Store<IStoreState>({
    state: {
        schemaModule: {
            tables: new Array(),
        },
    },
    actions,
    mutations,
    getters,
    modules: {
        SchemaModule,
    },
});

export default store;
