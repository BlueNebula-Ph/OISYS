(function (module) {

    var customerService = function ($http, env) {

        var urlBase = env.baseUrl + "/api/customer";
        var dataFactory = {};

        dataFactory.getCustomerLookup = function () {
            var url = urlBase + "/lookup";
            return $http.get(url);
        };

        dataFactory.getCustomerLookupWithOrders = function (isDelivered) {
            var url = urlBase + "/lookupWithOrders";

            if (isDelivered) {
                url += "/true";
            }

            return $http.get(url);
        };

        dataFactory.fetchCustomers = function (filters) {
            var url = urlBase + "/search";
            return $http.post(url, filters);
        };

        dataFactory.getCustomer = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveCustomer = function (id, customer) {
            if (id == 0) {
                return $http.post(urlBase, customer);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, customer);
            }
        };

        dataFactory.deleteCustomer = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        dataFactory.fetchCustomerTransactions = function (filters) {
            var url = urlBase + "/getTransactions";
            return $http.post(url, filters);
        };

        return dataFactory;
    };

    module.factory("customerService", ["$http", "env", customerService]);

})(angular.module("oisys-app"));