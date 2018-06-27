import { Component, Prop, Vue } from 'vue-property-decorator';
import { ITable } from '@/store/SchemaModule';
import { IStore } from '@/store';
import { IStoreState } from '@/store';

@Component
export default class Navigation extends Vue
{
    public get tables(): ITable[]
    {
        return this.$store.getters.tables;
    }
}