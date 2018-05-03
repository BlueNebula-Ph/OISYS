(function (module) {
    var provinceService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/province";
        var dataFactory = {};

        dataFactory.getProvinceLookup = function () {
            var url = urlBase + "/lookup";
            return $http.get(url);
        };

        dataFactory.searchProvinces = function (search) {
            var url = urlBase + "/search";
            return $http.post(url, search);
        };

        dataFactory.getProvinceById = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveProvince = function (id, province) {
            if (id == 0) {
                return $http.post(urlBase, province);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, province);
            }
        };

        dataFactory.deleteProvince = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("provinceService", ["$http", "env", provinceService]);

})(angular.module("oisys-app"));