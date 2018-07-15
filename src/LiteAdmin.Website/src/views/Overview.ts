import { Component, Vue } from 'vue-property-decorator';
import { IStoreState } from '@/store';
import { IStore } from '@/store';
import VueRouter from 'vue-router';
import { Route } from 'vue-router';
import * as ActionTypes from '@/store/ActionTypes';
import * as ParamNames from '@/ParamNames';
import FormBase from '@/views/FormBase';
import { ITable } from '@/store/SchemaModule';
import { IColumn } from '@/store/SchemaModule';
import { stringHelper } from '@/helpers/StringHelper';
import { ITableItem } from '@/store/TableDataModule';

@Component
export default class Overview extends FormBase
{
    public showDialog: boolean = false;

    public selectedItemId: string = '';

    public created(): void
    {
        this.$store.dispatch(ActionTypes.getTableItems, this.tableName);
    }

    public mounted(): void
    {
        this.$store.dispatch(ActionTypes.getLookups, this.tableSchema);
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

    public get tableKey(): string
    {
        for (const column of this.tableSchema.columns)
        {
            if (column.isPrimaryKey)
            {
                return column.name;
            }
        }

        return 'id';
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

    public removeItem(id: string): void
    {
        this.selectedItemId = id;
        this.showDialog = true;
    }

    public confirm(): void
    {
        const payload: ITableItem = {
            itemId: this.selectedItemId,
            tableName: this.tableName,
        };
        this.$store.dispatch(ActionTypes.deleteTableItem, payload   );
    }

    public cancel(): void
    {
        this.showDialog = false;
    }
}
