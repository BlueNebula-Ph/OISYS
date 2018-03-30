(function (module) {
    var addReturnsController = function () {
        var vm = this;

        // Helper properties
        vm.overrideDefaultControls = true;

        return vm;
    };

    module.controller("addReturnsController", [addReturnsController]);

})(angular.module("oisys-app"));