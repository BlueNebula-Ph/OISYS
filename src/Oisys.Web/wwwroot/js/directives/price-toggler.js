(function (module) {

    var priceToggler = function () {
        var priceTogglerController = ["$scope", function ($scope) {
            var vm = this;
            var priceTypes = ["mainPrice", "nePrice", "walkInPrice"];
            var currentIndex = 0;

            vm.togglePrices = function () {
                var nextIndex = ((currentIndex + 1) < priceTypes.length) ? (currentIndex + 1) : 0;

                vm.selectedPrice = $scope.selectedPrice = $scope.item[priceTypes[nextIndex]];

                currentIndex = nextIndex;
            };

            $scope.$watch("item", function (newVal, oldVal, scope) {
                var price = 0;
                if (newVal.id != 0) {
                    price = newVal[$scope.selectedPriceType];
                    currentIndex = priceTypes.indexOf($scope.selectedPriceType);
                }
                vm.selectedPrice = $scope.selectedPrice = price;
            }, true);

            return vm;
        }];

        return {
            restrict: "E",
            templateUrl: "/views/common/price-toggler.html?" + $.now(),
            scope: {
                item: "=",
                selectedPriceType: "@",
                selectedPrice: "="
            },
            controller: priceTogglerController,
            controllerAs: "ctrl"
        };
    };

    module.directive("priceToggler", [priceToggler]);

})(angular.module("oisys-app"));