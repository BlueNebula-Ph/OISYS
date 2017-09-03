(function (module) {

    var detailsShell = function () {
        return {
            restrict: "E",
            transclude: {
                title: "shellTitle",
                details: "shellDetails"
            },
            templateUrl: "/views/common/details-shell.html?" + $.now()
        };
    };

    module.directive("detailsShell", [detailsShell]);

})(angular.module("oisys-app"));