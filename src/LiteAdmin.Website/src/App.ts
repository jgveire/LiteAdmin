import { Vue, Watch } from 'vue-property-decorator';
import { Component} from 'vue-property-decorator';
import { ITable } from '@/store/SchemaModule';
import { IStore } from '@/store';
import { IStoreState } from '@/store';
import * as ActionTypes from '@/store/ActionTypes';

@Component
export default class App extends Vue
{
    public $store!: IStore<IStoreState>;

    public showSnackbar: boolean = false;

    public created(): void
    {
        this.$store.dispatch(ActionTypes.getSchema);
    }

    public get tables(): ITable[]
    {
        return this.$store.getters.tables;
    }

    public get showSnackbarFromStore(): boolean
    {
        return this.$store.getters.showSnackbar;
    }

    @Watch('showSnackbarFromStore')
    public showSnackbarWatch(value: boolean): void
    {
        this.showSnackbar = value;
    }

    public get snackbarMessage(): string
    {
        return this.$store.getters.snackbarMessage;
    }
}
