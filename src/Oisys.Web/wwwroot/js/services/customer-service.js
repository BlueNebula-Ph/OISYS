(function () {
    var customerService = function () {
        var urlBase = '/api/customers';
        var dataFactory = {};

        dataFactory.fetchAllCustomers = function () {
            var url = urlBase;
            return $http.get(url);
        };

        dataFactory.fetchCustomers = function (page, sort, search) {
            var url = urlBase + '?page=' + page + '&sort=' + sort + '&search=' + search;
            return $http.get(url);
        };

        dataFactory.getCustomer = function (id) {
            var url = urlBase + '/' + id;
            return $http.get(url);
        };

        dataFactory.saveCustomer = function (id, customer) {
            if (id === 0) {
                return $http.post(urlBase, { customer: customer });
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, customer);
            }
        };

        dataFactory.deleteCustomer = function (id) {
            return $http.delete(url, { id: id });
        };

        return dataFactory;
    };

    angular.module("oisys-app").factory("customerService", [customerService]);
})();