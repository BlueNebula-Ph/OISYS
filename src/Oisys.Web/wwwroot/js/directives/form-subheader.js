(function (module) {
    var formSubheader = function () {
        return {
            restrict: 'E',
            scope: {
                headerText: '@'
            },
            templateUrl: "/views/common/form-subheader.html?" + $.now()
        };
    };

    module.directive("formSubheader", [formSubheader]);

})(angular.module("oisys-app"));