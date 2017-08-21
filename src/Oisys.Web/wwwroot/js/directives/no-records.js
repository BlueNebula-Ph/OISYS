(function (module) {

    var noRecords = function () {
        return {
            restrict: "A",
            templateUrl: "/views/common/no-records.html?" + $.now(),
            scope: {
                colspan: "@"
            }
        };
    };

    module.directive("noRecords", [noRecords]);

})(angular.module("oisys-app"));