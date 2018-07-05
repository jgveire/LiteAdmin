import Vue from 'vue';
import Vuex from 'vuex';
import { Store } from 'vuex';
import { MutationTree } from 'vuex';
import { ActionTree } from 'vuex';
import { GetterTree } from 'vuex';
import SchemaModule from '@/store/SchemaModule';
import { ISchemaState } from '@/store/SchemaModule';
import { ISchemaGetters } from '@/store/SchemaModule';
import TableDataModule from '@/store/TableDataModule';
import { ITableDataState } from '@/store/TableDataModule';
import { ITableDataGetters } from '@/store/TableDataModule';
import SnackbarModule from '@/store/SnackbarModule';
import { ISnackbarState } from '@/store/SnackbarModule';
import { ISnackbarGetters } from '@/store/SnackbarModule';

Vue.use(Vuex);

export interface IStore<T> extends Store<T>
{
    getters: IStoreGetters;
}

export interface IStoreState
{
    schema: ISchemaState;
    tableData: ITableDataState;
    snackbar: ISnackbarState;
}

// tslint:disable no-empty-interface
export interface IStoreGetters extends ISchemaGetters, ITableDataGetters, ISnackbarGetters
{
}

const actions: ActionTree<IStoreState, IStoreState> = {
};

const getters: GetterTree<IStoreState, IStoreState> = {
};

const mutations: MutationTree<IStoreState> = {
};

const store: IStore<IStoreState> = new Vuex.Store<IStoreState>({
    state: {
        schema: {
            tables: new Array(),
        },
        tableData: {
            item: new Object(),
            items: new Array(),
            tableName: '',
        },
        snackbar: {
            showSnackbar: false,
            snackbarMessage: '',
        }
    },
    actions,
    mutations,
    getters,
    modules: {
        SchemaModule,
        TableDataModule,
        SnackbarModule
    },
});

export default store;
