(function () {
    var addCustomerController = function () {
        var vm = this;

        vm.customer = {};

        vm.save = function () {
            alert("Performed a save!");
        };

        return vm;
    };

    angular.module("oisys-app").controller("addCustomerController", [addCustomerController]);
})();