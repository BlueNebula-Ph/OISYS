(function (module) {
    var inventoryService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/item";
        var dataFactory = {};

        dataFactory.getItemLookup = function () {
            var url = urlBase + "/lookup";
            return $http.get(url);
        };

        dataFactory.fetchItems = function (filters) {
            var url = urlBase + "/search";
            return $http.post(url, filters);
        };

        dataFactory.getItem = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveItem = function (id, item) {
            if (id == 0) {
                return $http.post(urlBase, item);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, item);
            }
        };

        dataFactory.deletItem = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        dataFactory.adjustItemQuantity = function (id, adjustment) {
            var url = urlBase + "/" + id + "/adjust";
            return $http.post(url, adjustment);
        };

        return dataFactory;
    };

    module.factory("inventoryService", ["$http", "env", inventoryService]);

})(angular.module("oisys-app"));