(function (module) {

    var addOrderController = function () {
        var vm = this;

        vm.newOrderDetail = {};

        vm.orderDetails = [];

        vm.addOrderDetails = function () {
            alert("Test");
        };

        vm.save = function () { };

        vm.reset = function () { };

        return vm;
    };

    module.controller("addOrderController", [addOrderController]);

})(angular.module("oisys-app"));