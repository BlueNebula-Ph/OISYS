(function(module) {

    var summaryShell = function () {
        return {
            restrict: "E",
            transclude: {
                title: "shellTitle",
                search: "shellSearch",
                table: "shellTable"
            },
            templateUrl: "/views/common/summary-shell.html?" + $.now()
        };
    };

    module.directive("summaryShell", [summaryShell]);

})(angular.module("oisys-app"));