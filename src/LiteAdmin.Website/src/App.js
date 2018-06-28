import * as tslib_1 from "tslib";
import { Vue } from 'vue-property-decorator';
import { Component } from 'vue-property-decorator';
import * as ActionTypes from '@/store/ActionTypes';
import Navigation from '@/components/Navigation';
var App = /** @class */ (function (_super) {
    tslib_1.__extends(App, _super);
    function App() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    App.prototype.created = function () {
        this.$store.dispatch(ActionTypes.getSchema);
    };
    App = tslib_1.__decorate([
        Component({
            components: {
                Navigation: Navigation,
            },
        })
    ], App);
    return App;
}(Vue));
export default App;
//# sourceMappingURL=App.js.map