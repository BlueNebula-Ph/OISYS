(function (module) {
    var creditMemoService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/creditMemo";
        var dataFactory = {};

        dataFactory.fetchCreditMemos = function (filters) {
            var url = urlBase + "/search";
            return $http.post(url, filters);
        };

        dataFactory.getCreditMemo = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveCreditMemo = function (id, creditMemo) {
            if (id == 0) {
                return $http.post(urlBase, creditMemo);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, creditMemo);
            }
        };

        dataFactory.deleteCreditMemo = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("creditMemoService", ["$http", "env", creditMemoService]);

})(angular.module("oisys-app"));