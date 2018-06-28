import * as tslib_1 from "tslib";
import { Component, Vue } from 'vue-property-decorator';
var Table = /** @class */ (function (_super) {
    tslib_1.__extends(Table, _super);
    function Table() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    Object.defineProperty(Table.prototype, "tableName", {
        get: function () {
            return this.$route.params['tableName'];
        },
        enumerable: true,
        configurable: true
    });
    Table = tslib_1.__decorate([
        Component
    ], Table);
    return Table;
}(Vue));
export default Table;
//# sourceMappingURL=Table.js.map