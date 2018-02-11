(function (module) {
    var addQuotationController = function () {
        var vm = this;

        // Helper properties
        vm.overrideDefaultControls = true;
        vm.defaultFocus = true;

        return vm;
    };

    module.controller("addQuotationController", [addQuotationController]);

})(angular.module("oisys-app"));