import Axios from 'axios';
var dateFormat = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z?$/;
function reviver(key, value) {
    if (typeof (value) === 'string' && dateFormat.test(value)) {
        return new Date(value);
    }
    return value;
}
var ApiService = /** @class */ (function () {
    function ApiService() {
    }
    ApiService.httpClient = function () {
        var instance = Axios.create({
            baseURL: 'http://localhost:9000/liteadmin/api/',
            timeout: 5000,
            transformResponse: function (data) {
                if (data && typeof (data) === 'string') {
                    return JSON.parse(data, reviver);
                }
                return data;
            },
        });
        instance.interceptors.response.use(function (response) {
            return response;
        }, function (error) {
            alert(error);
            return Promise.reject(error);
        });
        return instance;
    };
    return ApiService;
}());
export default ApiService;
//# sourceMappingURL=ApiService.js.map