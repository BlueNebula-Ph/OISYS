(function (module) {

    var sidebar = function () {

        return {
            restrict: "E",
            templateUrl: "/views/common/sidebar.html?" + $.now(),
            scope: {
                items: "="
            }
        };
    };

    module.directive("sidebar", [sidebar]);

})(angular.module("oisys-app"));