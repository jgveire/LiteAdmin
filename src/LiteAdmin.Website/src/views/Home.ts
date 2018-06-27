import { Component, Vue } from 'vue-property-decorator';
import * as ActionTypes from '@/store/ActionTypes';
import Navigation from '@/components/Navigation';

@Component({
  components: {
      Navigation,
  },
})
export default class Home extends Vue
{
    private mounted(): void
    {
        //this.$store.dispatch(ActionTypes.getSchema);
    }
}
