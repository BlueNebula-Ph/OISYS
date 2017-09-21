(function (module) {
    var setupDom = function (element) {
        var innerSpan = element.querySelector("span");
        innerSpan.classList.add("input-group-addon");

        var input = element.querySelector("input, textarea, select");
        var type = input.getAttribute("type");

        if (type !== "checkbox" && type !== "radio") {
            input.classList.add("form-control");
            input.classList.add("input-sm");
        }

        element.classList.add("input-group");
        element.classList.add("input-group-sm");
    };

    var link = function (scope, element) {
        setupDom(element[0]);
    };

    var searchInput = function () {
        return {
            restrict: "A",
            link: link
        };
    };

    module.directive("searchInput", [searchInput]);

})(angular.module("oisys-app"));