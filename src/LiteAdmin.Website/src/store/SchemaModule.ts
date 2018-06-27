import { Module } from 'vuex';
import { MutationTree } from 'vuex';
import { GetterTree } from 'vuex';
import { ActionContext } from 'vuex';
import { ActionTree } from 'vuex';
import { IStoreState } from 'src/store/index';
import { SchemaService } from 'src/services';
import * as ActionTypes from 'src/store/ActionTypes';
import * as MutationTypes from 'src/store/MutationTypes';

export interface ITable
{
    name: string;
    columns: IColumn[];
}

export interface IColumn
{
    name: string;
}

export interface ISchemaState
{
    tables: ITable[];
}

export interface ISchemaGetters
{
    tables: ITable[];
}

const actions: ActionTree<ISchemaState, IStoreState> = {
    [ActionTypes.getSchema](context: ActionContext<ISchemaState, IStoreState>): Promise<void>
    {
        return new Promise<void>((resolve, reject) =>
        {
            SchemaService.getSchema()
                .then((tables: ITable[]) =>
                {
                    context.commit(MutationTypes.updateTables, tables);
                    resolve();
                })
                .catch(reject);
        });
    },
};

const getters: GetterTree<ISchemaState, IStoreState> = {

    tables(state: ISchemaState): ITable[]
    {
        return state.tables;
    },
};

const mutations: MutationTree<ISchemaState> = {
    [MutationTypes.updateTables](state: ISchemaState, payload: ITable[]): void
    {
        state.tables = payload;
    },
};

export const module: Module<ISchemaState, IStoreState> = {

    state: {
        tables: new Array(),
    },
    getters,
    actions,
    mutations,
};

export default module;
