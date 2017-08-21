(function (module) {

    var pager = function () {

        var pagingController = ["$scope", "$timeout", function ($scope, $timeout) {
            var vm = this;

            vm.goToPrev = function () {
                if ($scope.currentPage != 1) {
                    vm.goToPage($scope.currentPage - 1);
                }
            };

            vm.goToNext = function () {
                if ($scope.currentPage < $scope.totalPages) {
                    var page = parseInt($scope.currentPage);
                    vm.goToPage(page + 1);
                }
            };

            vm.goToPage = function (page) {
                $scope.currentPage = page;
                $timeout(function () {
                    $scope.onPageChange();
                }, 50);
            };

            vm.getTotalPages = function () {
                var result = new Array();

                for (var i = 1, total = $scope.totalPages; i <= total; i++) {
                    result.push(i);
                }

                return result;
            };

            return vm;
        }];

        return {
            restrict: "E",
            templateUrl: "/views/common/pager.html?" + $.now(),
            controller: pagingController,
            controllerAs: "ctrl",
            scope: {
                currentPage: "=",
                totalPages: "=",
                onPageChange: "&",
                hasPreviousPage: "=",
                hasNextPage: "="
            }
        };
    };

    module.directive("pager", [pager]);

})(angular.module("oisys-app"));