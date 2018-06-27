import { SchemaService } from '@/services';
import * as ActionTypes from '@/store/ActionTypes';
import * as MutationTypes from '@/store/MutationTypes';
var actions = (_a = {},
    _a[ActionTypes.getSchema] = function (context) {
        return new Promise(function (resolve, reject) {
            SchemaService.getSchema()
                .then(function (tables) {
                context.commit(MutationTypes.updateTables, tables);
                resolve();
            })
                .catch(reject);
        });
    },
    _a);
var getters = {
    tables: function (state) {
        return state.tables;
    },
};
var mutations = (_b = {},
    _b[MutationTypes.updateTables] = function (state, payload) {
        state.tables = payload;
    },
    _b);
export var module = {
    state: {
        tables: new Array(),
    },
    getters: getters,
    actions: actions,
    mutations: mutations,
};
export default module;
var _a, _b;
//# sourceMappingURL=SchemaModule.js.map