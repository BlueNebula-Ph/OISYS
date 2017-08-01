(function (module) {

    var referenceService = function ($http, env) {

        var urlBase = env.baseUrl + "/api/reference";
        var dataFactory = {};

        dataFactory.fetchReferences = function (type) {

        };

        dataFactory.fetchReference = function (type, id) {

        };

        dataFactory.saveReference = function (type) {

        };

        dataFactory.deleteReference = function (type) {

        };

        return dataFactory;
    };

    module.factory("referenceService", ["$http", "env", referenceService]);

})(angular.module("oisys-app"));