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
export default class Edit extends Vue
{
    public $store!: IStore<IStoreState>;

    public $router!: VueRouter;

    public $route!: Route;

    public $refs!: any;

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
        if (maxLength === 0)
        {
            return '';
        }

        return maxLength.toString();
    }

    public getFriendlyName(columnName: string): string
    {
        return stringHelper.split(columnName);
    }

    public createItem(): any
    {
        const obj: any = new Object();
        for (const column of this.tableSchema.columns)
        {
            if (column.dataType === 'Int16' ||
                column.dataType === 'Int32' ||
                column.dataType === 'Int64')
            {
                const n: number = Number.parseInt(this.$refs[column.name][0].value);
                if (!Number.isNaN(n))
                {
                    obj[column.name] = n;
                }
            }
            else if (column.dataType === 'Decimal' ||
                column.dataType === 'Float' ||
                column.dataType === 'Double')
            {
                const i: number = Number.parseFloat(this.$refs[column.name][0].value);
                if (!Number.isNaN(i))
                {
                    obj[column.name] = i;
                }
            }
            else if (column.dataType === 'DateTime')
            {
                const date: number = Date.parse(this.$refs[column.name][0].value);
                if (Number.isNaN(date))
                {
                    obj[column.name] = date;
                }
            }
            else if (column.dataType === 'Boolean')
            {
                return this.stringToBoolean(this.$refs[column.name][0].value);
            }
            else
            {
                obj[column.name] = this.$refs[column.name][0].value;
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

    public save(): void
    {
        const obj: any = this.createItem();
        const payload: IUpdateTableItem = {
            tableName: this.tableName,
            itemId: this.itemId,
            item: obj,
        };
        this.$store.dispatch(ActionTypes.updateTableItem, payload)
            .then(() => this.close());
    }

    public cancel(): void
    {
        this.close();
    }

    public close(): void
    {
        const path: string = '/tables/' + this.tableName;
        this.$router.push(path);
    }
}
