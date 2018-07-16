import { Module } from 'vuex';
import { MutationTree } from 'vuex';
import { GetterTree } from 'vuex';
import { ActionContext } from 'vuex';
import { ActionTree } from 'vuex';
import { IStoreState } from '@/store/index';
import { LookupService } from '@/services';
import { ILookup } from '@/services';
import { ITable } from '@/store/SchemaModule';
import { IUpdateTableItem, IAddTableItem, ITableItem } from '@/store/TableDataModule';
import * as ActionTypes from '@/store/ActionTypes';
import * as MutationTypes from '@/store/MutationTypes';

export interface IUpdateLookup
{
    items: ILookup[];
    name: string;
}

export interface ILookupState
{
    lookups: any;
}

export interface ILookupGetters
{
    lookups: any;
}

const actions: ActionTree<ILookupState, IStoreState> = {
    [ActionTypes.getLookups](context: ActionContext<ILookupState, IStoreState>, table: ITable): void
    {
        for (const column of table.columns)
        {
            if (column.foreignTable !== null)
            {
                context.commit(MutationTypes.initLookup, column.foreignTable);
                LookupService.getLookup(column.foreignTable)
                    .then((items: ILookup[]) =>
                    {
                        if (column.foreignTable !== null)
                        {
                            const payload: IUpdateLookup = {
                                items,
                                name: column.foreignTable,
                            };
                            context.commit(MutationTypes.updateLookup, payload);
                        }
                    });
            }
        }
    },

    [ActionTypes.refreshLookup](context: ActionContext<ILookupState, IStoreState>, tableName: string): Promise<void>
    {
        return new Promise<void>((resolve, reject) =>
        {
            LookupService.getLookup(tableName)
                .then((items: ILookup[]) =>
                {
                    const payload: IUpdateLookup = {
                        items,
                        name: tableName,
                    };
                    context.commit(MutationTypes.updateLookup, payload);
                    resolve();
                })
                .catch(reject);
        });
    },

    [ActionTypes.addTableItem](context: ActionContext<ILookupState, IStoreState>, payload: IAddTableItem): void
    {
        if (context.state.lookups[payload.tableName])
        {
            context.commit(ActionTypes.refreshLookup, payload.tableName);
        }
    },

    [ActionTypes.updateTableItem](context: ActionContext<ILookupState, IStoreState>, payload: IUpdateTableItem): void
    {
        if (context.state.lookups[payload.tableName])
        {
            context.commit(ActionTypes.refreshLookup, payload.tableName);
        }
    },

    [ActionTypes.deleteTableItem](context: ActionContext<ILookupState, IStoreState>, payload: ITableItem): void
    {
        if (context.state.lookups[payload.tableName])
        {
            context.commit(ActionTypes.refreshLookup, payload.tableName);
        }
    },
};

const getters: GetterTree<ILookupState, IStoreState> = {

    lookups(state: ILookupState): any
    {
        return state.lookups;
    },
};

const mutations: MutationTree<ILookupState> = {
    [MutationTypes.updateLookup](state: ILookupState, payload: IUpdateLookup): void
    {
        state.lookups[payload.name] = payload.items;
    },

    [MutationTypes.initLookup](state: ILookupState, lookupName: string): void
    {
        if (state.lookups[lookupName] === undefined)
        {
            state.lookups[lookupName] = new Array();
        }
    },
};

export const module: Module<ILookupState, IStoreState> = {

    state: {
        lookups: new Object(),
    },
    getters,
    actions,
    mutations,
};

export default module;
