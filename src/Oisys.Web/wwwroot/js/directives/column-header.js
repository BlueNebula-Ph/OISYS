(function (module) {

    var columnHeader = function () {

        var headerController = ['$scope', '$timeout', function ($scope, $timeout) {

            $scope.performSort = function (sort) {
                if ($scope.sort == sort) {
                    $scope.sortDirection = ($scope.sortDirection == 'asc' ? 'desc' : 'asc');
                } else {
                    $scope.sort = sort;
                    $scope.sortDirection = 'asc';
                }
                $timeout(function () { $scope.fetchCallBack() }, 50);
            };

            $scope.glyphVisible = function (sort) {
                if ($scope.sort == sort) {
                    return true;
                } else {
                    return false;
                }
            };

            $scope.upVisible = function () {
                return $scope.sortDirection == 'asc';
            };

            $scope.downVisible = function () {
                return $scope.sortDirection == 'desc';
            };

        }];

        return {
            restrict: 'E',
            templateUrl: '/views/common/column-header.html',
            scope: {
                headerText: '@',
                headerValue: '@',
                fetchCallBack: '&',
                sort: '=',
                sortDirection: '='
            },
            controller: headerController,
            controllerAs: "ctrl"
        };
    };

    module.directive("columnHeader", [columnHeader]);

})(angular.module("oisys-app"));