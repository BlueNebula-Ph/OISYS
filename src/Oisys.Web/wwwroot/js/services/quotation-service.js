(function (module) {
    var quotationService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/salesquote";
        var dataFactory = {};

        dataFactory.fetchQuotations = function (filters) {
            var url = urlBase + "/search";
            return $http.post(url, filters);
        };

        dataFactory.getQuotation = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveQuotation = function (id, quotation) {
            if (id == 0) {
                return $http.post(urlBase, quotation);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, quotation);
            }
        };

        dataFactory.deleteQuotation = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("quotationService", ["$http", "env", quotationService]);

})(angular.module("oisys-app"));