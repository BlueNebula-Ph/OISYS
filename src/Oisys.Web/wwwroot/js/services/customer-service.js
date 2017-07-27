(function (module) {
    var customerService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/customer";
        var dataFactory = {};

        dataFactory.fetchAllCustomers = function () {
            return $http.get(urlBase);
        };

        dataFactory.fetchCustomers = function (page, sort, search) {
        };

        dataFactory.getCustomer = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveCustomer = function (id, customer) {
            if (id === 0) {
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

        return dataFactory;
    };

    module.factory("customerService", ["$http", "env", customerService]);

})(angular.module("oisys-app"));