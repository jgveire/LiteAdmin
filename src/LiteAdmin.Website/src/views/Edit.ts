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
import FormBase from '@/views/FormBase';

@Component
export default class Edit extends FormBase
{
    public mounted(): void
    {
        const payload: ITableItem = {
            tableName: this.tableName,
            itemId: this.itemId,
        };
        this.$store.dispatch(ActionTypes.getTableItem, payload);
    }

    public get itemId(): string
    {
        return this.$route.params[ParamNames.id];
    }

    public get item(): any[]
    {
        return this.$store.getters.item;
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
}
