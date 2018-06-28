import * as tslib_1 from "tslib";
import { Component, Vue } from 'vue-property-decorator';
var Navigation = /** @class */ (function (_super) {
    tslib_1.__extends(Navigation, _super);
    function Navigation() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    Object.defineProperty(Navigation.prototype, "tables", {
        get: function () {
            return this.$store.getters.tables;
        },
        enumerable: true,
        configurable: true
    });
    Navigation = tslib_1.__decorate([
        Component
    ], Navigation);
    return Navigation;
}(Vue));
export default Navigation;
//# sourceMappingURL=Navigation.js.map