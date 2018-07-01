(function (module) {

    var invoiceService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/invoice";
        var dataFactory = {};

        dataFactory.fetchInvoices = function (filters) {
            var url = urlBase + "/search";
            return $http.post(url, filters);
        };

        dataFactory.getInvoice = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveInvoice = function (id, invoice) {
            if (id == 0) {
                return $http.post(urlBase, invoice);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, invoice);
            }
        };

        dataFactory.deleteInvoice = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("invoiceService", ["$http", "env", invoiceService]);

})(angular.module("oisys-app"));