import { Component, Vue } from 'vue-property-decorator';
import { IStore } from '@/store';
import { IStoreState } from '@/store';
import * as ActionTypes from '@/store/ActionTypes';

@Component
export default class App extends Vue
{
    public created(): void
    {
        this.$store.dispatch(ActionTypes.getSchema);
    }
}
