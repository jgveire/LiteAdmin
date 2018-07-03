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
import { IAddTableItem } from '@/store/TableDataModule';
import { stringHelper } from '@/helpers/StringHelper';
import FormBase from '@/views/FormBase';

@Component
export default class Edit extends FormBase
{
    public sending: boolean = false;

    public validateUser(): boolean
    {
        return false;
    }

    public save(): void
    {
        const obj: any = this.createItem();
        const payload: IAddTableItem = {
            tableName: this.tableName,
            item: obj,
        };
        this.$store.dispatch(ActionTypes.addTableItem, payload)
            .then(() => this.close());
    }

    public cancel(): void
    {
        this.close();
    }
}
