import * as tslib_1 from "tslib";
import { Component, Prop, Vue } from 'vue-property-decorator';
var HelloWorld = /** @class */ (function (_super) {
    tslib_1.__extends(HelloWorld, _super);
    function HelloWorld() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    tslib_1.__decorate([
        Prop(),
        tslib_1.__metadata("design:type", String)
    ], HelloWorld.prototype, "msg", void 0);
    HelloWorld = tslib_1.__decorate([
        Component
    ], HelloWorld);
    return HelloWorld;
}(Vue));
export default HelloWorld;
//# sourceMappingURL=HelloWorld.js.map