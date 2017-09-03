(function (module) {

    var addOrderController = function () {
        var vm = this;

        vm.newOrderDetail = {};

        vm.orderDetails = [
            //{ id: 0, Qty: 1, Item: "New Item", UnitPrice: 2333.23, TotalPrice: 2333.23 }
        ];

        vm.addOrderDetails = function () {
            vm.newOrderDetail.Unit = "pcs";
            vm.orderDetails.push(vm.newOrderDetail);
            vm.newOrderDetail = {};
        };

        vm.save = function () { };

        vm.reset = function () { };

        return vm;
    };

    module.controller("addOrderController", [addOrderController]);

})(angular.module("oisys-app"));