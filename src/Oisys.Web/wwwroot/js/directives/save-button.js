(function (module) {
    var saveButton = function () {
        return {
            restrict: "E",
            templateUrl: "/views/common/save-button.html?" + $.now(),
            scope: {
                onSave: "&",
                isDisabled: "=",
                isSaving: "="
            }
        };
    };

    module.directive("saveButton", [saveButton]);

})(angular.module("oisys-app"));