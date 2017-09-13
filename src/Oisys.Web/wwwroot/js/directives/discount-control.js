(function (module) {

    var discountControl = function () {
        return {
            restrict: "E",
            templateUrl: "/views/common/discount-control.html?" + $.now(),
            scope: {
                discountPercent: "=",
                discountAmount: "="
            }
        };
    };

    module.directive("discountControl", [discountControl]);

})(angular.module("oisys-app"));