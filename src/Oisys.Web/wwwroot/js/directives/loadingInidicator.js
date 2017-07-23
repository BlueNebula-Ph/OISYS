(function () {
    var loadingIndicator = function (loadingService) {
        var loadingController = ["$scope", "loadingService", function ($scope, loadingService) {
            $scope.showIndicator = loadingService.isShown;

            $scope.$watch(function () { return loadingService.isShown; }, function (newVal, oldVal) {
                $scope.showIndicator = newVal;
            });
        }];
        return {
            restrict: "E",
            templateUrl: "/views/common/loading-indicator.html?" + $.now(),
            controller: loadingController,
            scope: {
                loadingText: "@",
                showIndicator: "=?"
            },
            link: function (scope) {
                if (angular.isUndefined(scope.loadingText) || scope.loadingText == '') {
                    scope.loadingText = 'Loading';
                }
            }
        };
    };

    angular.module("oisys-app").directive("loadingIndicator", [loadingIndicator]);
})();