import { Component, Vue } from 'vue-property-decorator';
import { IStoreState } from '@/store';
import { IStore } from '@/store';
import VueRouter from 'vue-router';
import { Route } from 'vue-router';
import * as ActionTypes from '@/store/ActionTypes';
import * as ParamNames from '@/ParamNames';
import { ITable } from '@/store/SchemaModule';
import { IColumn } from '@/store/SchemaModule';
import { stringHelper } from '@/helpers/StringHelper';

@Component
export default class Overview extends Vue
{
    public $store!: IStore<IStoreState>;

    public $router!: VueRouter;

    public $route!: Route;

    public created(): void
    {
        this.$store.dispatch(ActionTypes.getTableItems, this.tableName);
    }

    public get tableName(): string
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
    }

    public edit(item: any): void
    {
        for (const column of this.tableSchema.columns)
        {
            if (column.isPrimaryKey)
            {
                const id: any = item[column.name];
                const path: string = '/maintain/' + this.tableName + '/edit/' + id.toString();
                this.$router.push(path);
                return;
            }
        }
    }

    public getFriendlyName(name: string): string
    {
        return stringHelper.split(name);
    }
}
