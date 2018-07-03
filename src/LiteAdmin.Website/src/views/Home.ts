import { Vue } from 'vue-property-decorator';
import { Component } from 'vue-property-decorator';
import { IStore } from '@/store';
import { IStoreState } from '@/store';
import { ITable } from '@/store/SchemaModule';

@Component
export default class Home extends Vue
{
    public $store!: IStore<IStoreState>;

    public get tables(): ITable[]
    {
        return this.$store.getters.tables;
    }

    public cardClick(event: MouseEvent): void
    {
        var tableName: string | null = null;
        var element: Element | null = event.srcElement;
        while (tableName === null && element !== null)
        {
            tableName = element.getAttribute('data-table-name');
            element = element.parentElement;
        }

        if (tableName !== null)
        {
            const path: string = '/maintain/' + tableName;
            this.$router.push(path);
        }
    }
}
