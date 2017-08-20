(function (module) {

    var formControl = function () {

        var controller = ["$scope", function ($scope) {
            var vm = this;

            vm.onSave = function () {
                $scope.onSave();
            };

            vm.onReset = function () {
                $scope.onReset();
            };

            return vm;
        }];

        return {
            restrict: "E",
            templateUrl: "/views/common/form-control.html?" + $.now(),
            scope: {
                onSave: "&",
                onReset: "&"
            },
            controller: controller,
            controllerAs: "ctrl"
        };
    };

    module.directive("formControl", [formControl]);

})(angular.module("oisys-app"));