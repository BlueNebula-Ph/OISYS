(function (module) {
    var cashVoucherService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/cashVoucher";
        var dataFactory = {};

        dataFactory.fetchCashVouchers = function (filters) {
            var url = urlBase + "/search";
            return $http.post(url, filters);
        };

        dataFactory.getCashVoucher = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveCashVoucher = function (id, cashVoucher) {
            if (id == 0) {
                return $http.post(urlBase, cashVoucher);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, cashVoucher);
            }
        };

        dataFactory.deleteCashVoucher = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("cashVoucherService", ["$http", "env", cashVoucherService]);

})(angular.module("oisys-app"));