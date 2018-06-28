import { Vue } from 'vue-property-decorator';
import { Component} from 'vue-property-decorator';
import { IStore } from '@/store';
import { IStoreState } from '@/store';
import * as ActionTypes from '@/store/ActionTypes';
import Navigation from '@/components/Navigation';

@Component
export default class App extends Vue
{
    public $store!: IStore<IStoreState>;

    public created(): void
    {
        this.$store.dispatch(ActionTypes.getSchema);
    }
}
