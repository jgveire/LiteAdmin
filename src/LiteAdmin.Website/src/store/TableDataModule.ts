import { Module } from 'vuex';
import { MutationTree } from 'vuex';
import { GetterTree } from 'vuex';
import { ActionContext } from 'vuex';
import { ActionTree } from 'vuex';
import { IStoreState } from '@/store/index';
import { TableDataService } from '@/services';
import * as ActionTypes from '@/store/ActionTypes';
import * as MutationTypes from '@/store/MutationTypes';

export interface ITableDataState
{
    tableName: string;
    item: any;
    items: any[];
}

export interface ITableDataGetters
{
    tableName: string;
    item: any;
    items: any[];
}

export interface IGetTableItem
{
    itemId: string;
    tableName: string;
}

export interface ITableItem
{
    itemId: string;
    item: any;
    tableName: string;
}

export interface IUpdateTableItem extends  ITableItem
{
    item: any;
}

export interface IAddTableItem
{
    tableName: string;
    item: any;
}

const actions: ActionTree<ITableDataState, IStoreState> = {
    [ActionTypes.getTableItems](context: ActionContext<ITableDataState, IStoreState>, tableName: string): Promise<void>
    {
        if (context.getters.tableName !== tableName)
        {
            context.commit(MutationTypes.updateTableName, tableName);
            context.commit(MutationTypes.updateTableItems, new Array());
        }

        return new Promise<void>((resolve, reject) =>
        {
            TableDataService.getItems(tableName)
                .then((items: any[]) =>
                {
                    context.commit(MutationTypes.updateTableItems, items);
                    resolve();
                })
                .catch(reject);
        });
    },

    [ActionTypes.getTableItem](context: ActionContext<ITableDataState, IStoreState>, payload: IGetTableItem): Promise<void>
    {
        context.commit(MutationTypes.updateTableName, payload.tableName);
        context.commit(MutationTypes.updateTableItem, new Object());

        return new Promise<void>((resolve, reject) =>
        {
            TableDataService.getItem(payload.tableName, payload.itemId)
                .then((item: any) =>
                {
                    context.commit(MutationTypes.updateTableItem, item);
                    resolve();
                })
                .catch(reject);
        });
    },

    [ActionTypes.updateTableItem](context: ActionContext<ITableDataState, IStoreState>, payload: IUpdateTableItem): Promise<void>
    {
        return new Promise<void>((resolve, reject) =>
        {
            TableDataService.updateItem(payload.tableName, payload.itemId, payload.item)
                .then(resolve)
                .catch(reject);
        });
    },

    [ActionTypes.addTableItem](context: ActionContext<ITableDataState, IStoreState>, payload: IAddTableItem): Promise<void>
    {
        context.commit(MutationTypes.addTableItem, payload.item);
        return new Promise<void>((resolve, reject) =>
        {
            TableDataService.addItem(payload.tableName, payload.item)
                .then(resolve)
                .catch(reject);
        });
    },

    [ActionTypes.deleteTableItem](context: ActionContext<ITableDataState, IStoreState>, payload: ITableItem): Promise<void>
    {
        context.commit(MutationTypes.deleteTableItem, payload.item);
        return new Promise<void>((resolve, reject) =>
        {
            TableDataService.deleteItem(payload.tableName, payload.itemId)
                .then(resolve)
                .catch(reject);
        });
    },
};

const getters: GetterTree<ITableDataState, IStoreState> = {

    items(state: ITableDataState): any[]
    {
        return state.items;
    },

    item(state: ITableDataState): any[]
    {
        return state.item;
    },
};

const mutations: MutationTree<ITableDataState> = {
    [MutationTypes.updateTableItems](state: ITableDataState, items: any[]): void
    {
        state.items = items;
    },

    [MutationTypes.updateTableItem](state: ITableDataState, item: any): void
    {
        state.item = item;
    },

    [MutationTypes.updateTableName](state: ITableDataState, tableName: string): void
    {
        state.tableName = tableName;
    },

    [MutationTypes.addTableItem](state: ITableDataState, item: any): void
    {
        state.items.push(item);
    },

    [MutationTypes.deleteTableItem](state: ITableDataState, item: any): void
    {
        for (let i = 0; i < state.items.length; i++)
        {
            if (state.items[i] === item)
            {
                state.items.splice(i, 1);
                return;
            }
        }
    },
};

export const module: Module<ITableDataState, IStoreState> = {

    state: {
        item: new Object(),
        items: new Array(),
        tableName: '',
    },
    getters,
    actions,
    mutations,
};

export default module;
