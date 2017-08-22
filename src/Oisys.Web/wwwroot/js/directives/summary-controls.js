(function (module) {

    var summaryControls = function () {

        return {
            restrict: "E",
            templateUrl: "/views/common/summary-controls.html?" + $.now(),
            scope: {
                showDetails: "=",
                detailsState: "@",
                detailsTitle: "@",

                showEdit: "=",
                editState: "@",
                editTitle: "@",

                showDelete: "=",
                onDelete: "&",
                deleteTitle: "@"
            }
        };
    };

    module.directive("summaryControls", [summaryControls]);

})(angular.module("oisys-app"));