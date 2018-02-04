(function (module) {
    var userService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/user";
        var dataFactory = {};

        dataFactory.getUserLookup = function () {
            var url = urlBase + "/lookup";
            return $http.get(url);
        };

        dataFactory.searchUsers = function (search) {
            var url = urlBase + "/search";
            return $http.post(url, search);
        };

        dataFactory.getUserById = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveUser = function (id, user) {
            if (id == 0) {
                return $http.post(urlBase, user);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, user);
            }
        };

        dataFactory.deleteUser = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("userService", ["$http", "env", userService]);

})(angular.module("oisys-app"));