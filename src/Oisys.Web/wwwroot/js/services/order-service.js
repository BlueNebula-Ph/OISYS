(function (module) {

    var orderService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/order";
        var dataFactory = {};

        dataFactory.fetchOrders = function (page, sortBy, sortDirection, search) {
            var url = urlBase + "/search";
            var filter = { sortBy: sortBy, sortDirection: sortDirection };

            return $http.post(url, filter);
        };

        dataFactory.getOrder = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveOrder = function (id, order) {
            if (id === 0) {
                return $http.post(urlBase, order);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, order);
            }
        };

        dataFactory.deleteOrder = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("orderService", ["$http", "env", orderService]);

})(angular.module("oisys-app"));