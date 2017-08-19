(function (module) {

    var referenceService = function ($http, env) {

        var urlBase = env.baseUrl + "/api/reference";
        var dataFactory = {};

        dataFactory.getAllReferences = function (referenceTypeId) {

        };

        dataFactory.fetchReferences = function (referenceTypeId, search) {
            var url = urlBase + "/" + referenceTypeId + "/search";
            return $http.post(url, search);
        };

        dataFactory.fetchReference = function (type, id) {

        };

        dataFactory.saveReference = function (id, reference) {
            if (id === 0) {
                return $http.post(urlBase, reference);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, reference);
            }
        };

        dataFactory.deleteReference = function (type) {

        };

        return dataFactory;
    };

    module.factory("referenceService", ["$http", "env", referenceService]);

})(angular.module("oisys-app"));