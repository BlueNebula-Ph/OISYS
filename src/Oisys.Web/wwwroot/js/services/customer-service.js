(function () {
    var customerService = function ($http) {
        var urlBase = 'http://localhost:57334/api/customer';
        var dataFactory = {};

        dataFactory.fetchAllCustomers = function () {
            return $http.get(urlBase);
        };

        dataFactory.fetchCustomers = function (page, sort, search) {
            var url = urlBase + '?page=' + page + '&sort=' + sort + '&search=' + search;
            return $http.get(url);
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

    angular.module("oisys-app").factory("customerService", ["$http", customerService]);
})();