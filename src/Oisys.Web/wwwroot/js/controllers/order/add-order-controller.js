(function (module) {
    var addOrderController = function (customerService, inventoryService, orderService, utils, $stateParams, $scope, $q, Order, OrderDetail) {
        var vm = this;

        // Main properties
        vm.order = new Order();

        // Lists
        vm.customerList = [];
        vm.itemList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;
        vm.overrideDefaultControls = true;

        // Public methods
        vm.addOrderDetail = function () {
            var detail = new OrderDetail();
            vm.order.details.splice(0, 0, detail);
        };

        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            // Update for edit
            orderService.saveOrder(0, vm.order)
                .then(onSaveSuccess, utils.onError)
                .finally(onSaveComplete);
        };

        vm.editDetail = function (detail) {
            detail.showEdit = true;
        };

        vm.saveDetail = function (detail) {
            detail.showEdit = false;
        }

        vm.deleteDetail = function (detail) {
            //vm.order.details.splice(index);
            detail.isDeleted = true;
        };

        vm.undoDelete = function (detail) {
            detail.isDeleted = false;
        };

        // Private methods
        var onSaveSuccess = function (response) {
            utils.showSuccessMessage("Order saved successfully.");
        };

        var onSaveComplete = function () {
            vm.saveEnabled = true;
            utils.hideLoading();

            vm.order = new Order();
            console.log(vm.order);
        };
        
        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup()
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "name", "-- Select Customer --");
                    utils.populateDropdownlist(responses.item, vm.itemList, "", "");
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            initialLoad();
        });

        return vm;
    };

    module.controller("addOrderController", ["customerService", "inventoryService", "orderService", "utils", "$stateParams", "$scope", "$q", "Order", "OrderDetail", addOrderController]);

})(angular.module("oisys-app"));