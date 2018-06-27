import * as tslib_1 from "tslib";
import ApiService from '@/services/ApiService';
var SchemaService = /** @class */ (function (_super) {
    tslib_1.__extends(SchemaService, _super);
    function SchemaService() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SchemaService.getSchema = function () {
        var _this = this;
        return new Promise(function (resolve, reject) {
            var url = 'schema';
            _this.httpClient().get(url)
                .then(function (response) { return resolve(response.data); })
                .catch(reject);
        });
    };
    return SchemaService;
}(ApiService));
export { SchemaService };
//# sourceMappingURL=SchemaService.js.map