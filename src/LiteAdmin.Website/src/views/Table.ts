import { Component, Vue } from 'vue-property-decorator';
import { IStoreState } from '@/store';
import { IStore } from '@/store';
import VueRouter from 'vue-router';
import { Route } from 'vue-router';
import * as ActionTypes from '@/store/ActionTypes';
import * as ParamNames from '@/ParamNames';
import { ITable } from '@/store/SchemaModule';

@Component
export default class Table extends Vue
{
    public $store!: IStore<IStoreState>;

    public $router!: VueRouter;

    public $route!: Route;

    public created(): void
    {
        this.$store.dispatch(ActionTypes.getSchema, this.tableName);
        this.$store.dispatch(ActionTypes.getTableItems, this.tableName);
    }

    public get tableName()
    {
        return this.$route.params[ParamNames.tableName];
    }

    public get tableSchema(): ITable
    {
        const tables: ITable[] = this.$store.getters.tables.filter((e) => e.name === this.tableName);
        if (tables.length === 1)
        {
            return tables[0];
        }

        return {
            name: this.tableName,
            columns: new Array(),
        };
    }

    public get items(): any[]
    {
        return this.$store.getters.items;
        //return ['dsf', 'sdfs'];
    }

}
