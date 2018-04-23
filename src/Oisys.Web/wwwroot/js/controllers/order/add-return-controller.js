(function (module) {
    var addReturnController = function () {
        var vm = this;

        // Helper properties
        vm.overrideDefaultControls = true;

        return vm;
    };

    module.controller("addReturnController", [addReturnController]);

})(angular.module("oisys-app"));