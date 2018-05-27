(function (module) {

    var deliveryService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/delivery";
        var dataFactory = {};

        dataFactory.fetchDeliveries = function (filters) {
            var url = urlBase + "/search";
            return $http.post(url, filters);
        };

        dataFactory.getDelivery = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveDelivery = function (id, delivery) {
            if (id == 0) {
                return $http.post(urlBase, delivery);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, delivery);
            }
        };

        dataFactory.deleteDelivery = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("deliveryService", ["$http", "env", deliveryService]);

})(angular.module("oisys-app"));