(function (module) {

    var addItemController = function () {
        var vm = this;
        vm.defaultFocus = true;
        vm.item = {};

        vm.save = function () {

        };

        vm.reset = function (form) {
            form.$setPristine();
        };

        return vm;
    };

    module.controller("addItemController", [addItemController]);

})(angular.module("oisys-app"));