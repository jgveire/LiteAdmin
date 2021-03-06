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
import { IUpdateTableItem } from '@/store/TableDataModule';
import { stringHelper } from '@/helpers/StringHelper';

@Component
export default class FormBase extends Vue
{
    public $store!: IStore<IStoreState>;

    public $router!: VueRouter;

    public $route!: Route;

    public $refs!: any;

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

    public getInputType(dataType: string): string
    {
        if (dataType === 'Int16' ||
            dataType === 'Int32' ||
            dataType === 'Int64' ||
            dataType === 'decimal' ||
            dataType === 'float')
        {
            return 'number';
        }
        else if (dataType === 'DateTime')
        {
            return 'date';
        }
        return 'text';
    }

    public getMaxLength(maxLength: number): string
    {
        if (maxLength <= 0)
        {
            return '';
        }

        return maxLength.toString();
    }

    public getFriendlyName(columnName: string): string
    {
        return stringHelper.split(columnName);
    }

    public createNewItem(): any
    {
        const obj: any = new Object();
        for (const column of this.tableSchema.columns)
        {
            if (column.dataType === 'Int16' ||
                column.dataType === 'Int32' ||
                column.dataType === 'Int64')
            {
                obj[column.name] = null;
            }
            else if (column.dataType === 'Decimal' ||
                column.dataType === 'Float' ||
                column.dataType === 'Double')
            {
                obj[column.name] = null;
            }
            else if (column.dataType === 'DateTime')
            {
                obj[column.name] = null;
            }
            else if (column.dataType === 'Byte' || column.dataType === 'Boolean')
            {
                obj[column.name] = false;
            }
            else
            {
                obj[column.name] = null;
            }
        }

        return obj;
    }

    public stringToBoolean(value: string): boolean | null
    {
        switch (value.toLowerCase().trim())
        {
        case 'true':
        case 'yes':
        case '1':
            return true;
        case 'false':
        case 'no':
        case '0':
        case null:
            return false;
        default:
            return null;
        }
    }

    public close(): void
    {
        const path: string = '/maintain/' + this.tableName;
        this.$router.push(path);
    }
}
