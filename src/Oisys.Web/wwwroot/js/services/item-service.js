(function (module) {

    var customerService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/item";
        var dataFactory = {};

        dataFactory.fetchItems = function (page, sortBy, sortDirection, search) {
            var url = urlBase + "/search";
            var filter = { sortBy: sortBy, sortDirection: sortDirection };

            return $http.post(url, filter);
        };

        dataFactory.getItem = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveItem = function (id, item) {
            if (id === 0) {
                return $http.post(urlBase, item);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, item);
            }
        };

        dataFactory.deleteItem = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("customerService", ["$http", "env", customerService]);

})(angular.module("oisys-app"));