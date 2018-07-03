import { Vue } from 'vue-property-decorator';
import { Component} from 'vue-property-decorator';
import { ITable } from '@/store/SchemaModule';
import { IStore } from '@/store';
import { IStoreState } from '@/store';
import * as ActionTypes from '@/store/ActionTypes';

@Component
export default class App extends Vue
{
    public $store!: IStore<IStoreState>;

    public created(): void
    {
        this.$store.dispatch(ActionTypes.getSchema);
    }

    public get tables(): ITable[]
    {
        return this.$store.getters.tables;
    }
}
