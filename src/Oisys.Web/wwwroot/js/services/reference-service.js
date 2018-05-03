(function (module) {
    var referenceService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/reference";
        var dataFactory = {};

        dataFactory.getReferenceLookup = function (referenceTypeId) {
            var url = urlBase + "/" + referenceTypeId + "/lookup";
            return $http.get(url);
        };

        return dataFactory;
    };

    module.factory("referenceService", ["$http", "env", referenceService]);

})(angular.module("oisys-app"));