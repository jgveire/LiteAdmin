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
import { ILookup } from '@/services/LookupService';
import moment from 'moment';

@Component
export default class Overview extends FormBase
{
    public showDialog: boolean = false;

    public selectedItemId: string = '';

    public selectedItem: any;

    public currentTable: string | null = null;

    public created(): void
    {
        this.$store.dispatch(ActionTypes.getLookups, this.tableSchema);
    }

    public mounted(): void
    {
        this.$store.dispatch(ActionTypes.getTableItems, this.tableName);
        this.currentTable = this.tableName;
    }

    public updated(): void
    {
        if (this.tableName && this.currentTable !== this.tableName)
        {
            this.$store.dispatch(ActionTypes.getLookups, this.tableSchema);
            this.$store.dispatch(ActionTypes.getTableItems, this.tableName);
            this.currentTable = this.tableName;
        }
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

    public removeItem(id: string, item: any): void
    {
        this.selectedItemId = id;
        this.selectedItem = item;
        this.showDialog = true;
    }

    public confirm(): void
    {
        const payload: ITableItem = {
            itemId: this.selectedItemId,
            item: this.selectedItem,
            tableName: this.tableName,
        };
        this.$store.dispatch(ActionTypes.deleteTableItem, payload);
    }

    public cancel(): void
    {
        this.showDialog = false;
    }

    public getDisplayValue(item: any, column: IColumn): any
    {
        if (column.foreignTable)
        {
            const lookup: ILookup[] = this.$store.getters.lookups[column.foreignTable];
            if (lookup)
            {
                const entries: ILookup[] = lookup.filter((e) => e.id === item[column.name]);
                if (entries.length === 1)
                {
                    return entries[0].name;
                }
            }
        }
        else if (column.dataType === 'DateTime' &&
            item[column.name])
        {
            const date: Date = item[column.name];
            if (date.getHours() === 0 &&
                date.getMinutes() === 0 &&
                date.getSeconds() === 0 &&
                date.getMilliseconds() === 0)
            {
                return moment(date).format('DD-MM-YYYY');
            }
            else
            {
                return moment(date).format('DD-MM-YYYY HH:mm:ss');
            }
        }

        return item[column.name];
    }
}
