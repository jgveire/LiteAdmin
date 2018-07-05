import { Module } from 'vuex';
import { MutationTree } from 'vuex';
import { GetterTree } from 'vuex';
import { ActionContext } from 'vuex';
import { ActionTree } from 'vuex';
import { IStoreState } from '@/store/index';
import * as ActionTypes from '@/store/ActionTypes';
import * as MutationTypes from '@/store/MutationTypes';

export interface ISnackbarState
{
    showSnackbar: boolean;
    snackbarMessage: string;
    timeoutId: number;
}

export interface ISnackbarGetters
{
    showSnackbar: boolean;
    snackbarMessage: string;
}

const actions: ActionTree<ISnackbarState, IStoreState> = {
    [ActionTypes.showSnackbar](context: ActionContext<ISnackbarState, IStoreState>, message: string): void
    {
        context.commit(MutationTypes.updateSnackbarMessage, message);
        context.commit(MutationTypes.showSnackbar, true);
        context.state.timeoutId = window.setTimeout(() => {
            context.commit(MutationTypes.showSnackbar, false);
        }, 4000);
    },

    [ActionTypes.hideSnackbar](context: ActionContext<ISnackbarState, IStoreState>): void
    {
        context.commit(MutationTypes.showSnackbar, false);
    },
};

const getters: GetterTree<ISnackbarState, IStoreState> = {

    showSnackbar(state: ISnackbarState): boolean
    {
        return state.showSnackbar;
    },

    snackbarMessage(state: ISnackbarState): string
    {
        return state.snackbarMessage;
    },
};

const mutations: MutationTree<ISnackbarState> = {
    [MutationTypes.updateSnackbarMessage](state: ISnackbarState, message: string): void
    {
        state.snackbarMessage = message;
    },

    [MutationTypes.showSnackbar](state: ISnackbarState, show: boolean): void
    {
        state.showSnackbar = show;
    },
};

export const module: Module<ISnackbarState, IStoreState> = {

    state: {
        showSnackbar: false,
        snackbarMessage: '',
        timeoutId: 0,
    },
    getters,
    actions,
    mutations,
};

export default module;
