(function (module) {

    var setupDom = function (element) {
        var input = element.querySelector("input, textarea, select");
        var type = input.getAttribute("type");

        if (type !== "checkbox" && type !== "radio") {
            input.classList.add("form-control");
            input.classList.add("input-sm");
        }

        var label = element.querySelector("label");
        label.classList.add("control-label");

        element.classList.add("form-group");
        element.classList.add("form-group-sm");
    };

    var link = function (scope, element) {
        setupDom(element[0]);
    };

    var formInput = function () {
        return {
            restrict: "A",
            link: link
        };
    };

    module.directive("formInput", [formInput]);

})(angular.module("oisys-app"));