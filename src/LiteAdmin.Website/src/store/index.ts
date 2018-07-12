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
import LookupModule from '@/store/LookupModule';
import { ILookupState } from '@/store/LookupModule';
import { ILookupGetters } from '@/store/LookupModule';
import * as MutationTypes from '@/store/MutationTypes';

Vue.use(Vuex);

export interface IStore<T> extends Store<T>
{
    getters: IStoreGetters;
}

export interface IStoreState
{
    apiUrl: string;
    schema: ISchemaState;
    tableData: ITableDataState;
    snackbar: ISnackbarState;
    lookup: ILookupState;
}

// tslint:disable no-empty-interface
export interface IStoreGetters extends ISchemaGetters, ITableDataGetters, ISnackbarGetters, ILookupGetters
{
    apiUrl: string;
}

const actions: ActionTree<IStoreState, IStoreState> = {
};

const getters: GetterTree<IStoreState, IStoreState> = {

    apiUrl(state: IStoreState): string
    {
        return state.apiUrl;
    },
};

const mutations: MutationTree<IStoreState> = {

    [MutationTypes.updateApiUrl](state: IStoreState, apiUrl: string): void
    {
        state.apiUrl = apiUrl;
    },
};

const store: IStore<IStoreState> = new Vuex.Store<IStoreState>({
    state: {
        apiUrl: '',
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
            timeoutId: 0,
        },
        lookup: {
            lookups: new Object(),
        },
    },
    actions,
    mutations,
    getters,
    modules: {
        SchemaModule,
        TableDataModule,
        SnackbarModule,
        LookupModule,
    },
});

export default store;
