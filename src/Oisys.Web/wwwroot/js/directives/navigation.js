(function (module) {

    var navigation = function () {
        return {
            restrict: "E",
            templateUrl: "/views/common/navigation.html?" + $.now()
        };
    };

    module.directive("navigation", [navigation]);

})(angular.module("oisys-app"));