import { Component, Vue } from 'vue-property-decorator';
import { IStoreState } from '@/store';
import { IStore } from '@/store';
import VueRouter from 'vue-router';
import { Route } from 'vue-router';
import * as ActionTypes from '@/store/ActionTypes';
import * as ParamNames from '@/ParamNames';
import { ITable } from '@/store/SchemaModule';
import { IColumn } from '@/store/SchemaModule';
import { ITableItem } from '@/store/TableDataModule';

@Component
export default class Details extends Vue
{
    public $store!: IStore<IStoreState>;

    public $router!: VueRouter;

    public $route!: Route;

    public mounted(): void
    {
        const payload: ITableItem = {
            tableName: this.tableName,
            itemId: this.itemId,
        };
        this.$store.dispatch(ActionTypes.getTableItem, payload);
    }

    public get tableName(): string
    {
        return this.$route.params[ParamNames.tableName];
    }

    public get itemId(): string
    {
        return this.$route.params[ParamNames.id];
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

    public get item(): any[]
    {
        return this.$store.getters.item;
    }
}
