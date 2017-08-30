(function (module) {
    var numericTextbox = function () {
        var options = {};
        return {
            require: "?ngModel",
            restrict: "A",
            compile: function (elem, attributes) {
                var isTextInput = elem.is("input:text");

                return function (scope, elm, attrs, controller) {
                    // Get instance-specific options.
                    var opts = angular.extend({}, options, scope.$eval(attrs.numericTextbox));

                    // Helper method to update autoNumeric with new value.
                    var updateElement = function (element, newVal) {
                        // Only set value if value is numeric
                        if ($.isNumeric(newVal))
                            element.autoNumeric("set", newVal);
                    };

                    // Initialize element as autoNumeric with options.
                    elm.autoNumeric(opts);

                    // if element has controller, wire it (only for <input type="text" />)
                    if (controller && isTextInput) {
                        // watch for external changes to model and re-render element
                        scope.$watch(attributes.ngModel, function (current, old) {
                            controller.$render();
                        });
                        // render element as autoNumeric
                        controller.$render = function () {
                            updateElement(elm, controller.$viewValue);
                        }
                        // Detect changes on element and update model.
                        elm.on("change", function (e) {
                            scope.$apply(function () {
                                controller.$setViewValue(elm.autoNumeric("get"));
                            });
                        });
                    } else {
                        // Listen for changes to value changes and re-render element.
                        // Useful when binding to a readonly input field.
                        if (isTextInput) {
                            attrs.$observe("value", function (val) {
                                updateElement(elm, val);
                            });
                        }
                    }
                }
            }
        };
    };

    module.directive("numericTextbox", [numericTextbox]);

})(angular.module("oisys-app"));