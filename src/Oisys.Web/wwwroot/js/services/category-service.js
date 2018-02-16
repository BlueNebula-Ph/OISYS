(function (module) {
    var categoryService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/category";
        var dataFactory = {};

        dataFactory.getCategoryLookup = function () {
            var url = urlBase + "/lookup";
            return $http.get(url);
        };

        dataFactory.fetchCategories = function (filters) {
            var url = urlBase + "/search";
            return $http.post(url, filters);
        };

        dataFactory.getCategory = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveCategory = function (id, category) {
            if (id == 0) {
                return $http.post(urlBase, category);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, category);
            }
        };

        dataFactory.deleteCategory = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("categoryService", ["$http", "env", categoryService]);

})(angular.module("oisys-app"));