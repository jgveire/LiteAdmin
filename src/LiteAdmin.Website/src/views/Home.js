import * as tslib_1 from "tslib";
import { Component, Vue } from 'vue-property-decorator';
import * as ActionTypes from '@/store/ActionTypes';
import HelloWorld from '@/components/HelloWorld';
var Home = /** @class */ (function (_super) {
    tslib_1.__extends(Home, _super);
    function Home() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    Home.prototype.mounted = function () {
        this.$store.dispatch(ActionTypes.getSchema);
    };
    Home = tslib_1.__decorate([
        Component({
            components: {
                HelloWorld: HelloWorld,
            },
        })
    ], Home);
    return Home;
}(Vue));
export default Home;
//# sourceMappingURL=Home.js.map