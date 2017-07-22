(function () {
    var sidebar = function () {
        var sidebarController = ["$scope", function ($scope) {
            var vm = this;

            vm.toggleSave = function () {
                $scope.toggleSave();
            };

            vm.toggleSearch = function () {
                $scope.toggleSearch();
            };

            return vm;
        }];

        return {
            restrict: "E",
            templateUrl: "/views/common/sidebar.html?" + $.now(),
            scope: {
                showSearch: "=",
                toggleSearch: "&",
                showAdd: "=",
                addTooltip: "@",
                addUrl: "@",
                showSave: "=",
                toggleSave: "&",
                cancelUrl: "@",
                showCancel: "=",
                showEdit: "=",
                editTooltip: "@",
                showDelete: "=",
                deleteTooltip: "@",
                showBack: "=",
                backUrl: "@"
            },
            controller: sidebarController,
            controllerAs: "ctrl"
        };
    };

    angular.module("oisys-app").directive("sidebar", [sidebar]);
})();