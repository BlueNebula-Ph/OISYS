(function (module) {
    var addOrderController = function (customerService, inventoryService, orderService, utils, $stateParams, $scope, $q) {
        var vm = this;

        vm.defaultFocus = true;
        vm.order = {
            customerId: 0,
            discountPercent: 0,
            discountAmount: 0,
            totalAmount: 0
        };
        vm.newOrderDetail = {
            id: 0,
            orderId: 0,
            itemId: 0,
            quantity: 1,
            selectedItem: { id: 0, codeName: "-- Select Item --" },
            price: 0,
            totalPrice: 0,
            focus: true,
            isDeleted: false
        };

        vm.orderDetails = [];
        vm.addOrderDetail = function () {
            var detail = angular.copy(vm.newOrderDetail);
            vm.orderDetails.splice(0, 0, detail);
        };

        vm.removeOrderDetail = function (index) {
            if (vm.orderDetails[index].id == 0) {
                vm.orderDetails.splice(index, 1);
            } else {
                vm.orderDetails[index].isDeleted = true;
            }
        };

        vm.undoRemove = function (index) {
            vm.orderDetails[index].isDeleted = false;
        };

        var assignValuesAndComputeTotals = function () {
            var totalAmount = 0;
            for (var i = 0; i < vm.orderDetails.length; i++) {
                // Assign the correct id and unit to the row
                vm.orderDetails[i].itemId = vm.orderDetails[i].selectedItem.id;
                vm.orderDetails[i].unit = vm.orderDetails[i].selectedItem.unit;

                if (!vm.orderDetails[i].isDeleted) {
                    var totalPrice = vm.orderDetails[i].quantity * vm.orderDetails[i].price;
                    vm.orderDetails[i].totalPrice = totalPrice;
                    totalAmount += totalPrice;
                }
            }

            var discountAmount = totalAmount * vm.order.discountPercent / 100;
            vm.order.discountAmount = discountAmount;
            vm.order.totalAmount = totalAmount - discountAmount;
        };

        $scope.$watch(function () {
            return vm.orderDetails;
        }, function (newVal, oldVal, scope) {
            assignValuesAndComputeTotals();
        }, true);

        $scope.$watch(function () {
            return vm.order.discountPercent;
        }, function (newVal, oldVal) {
            assignValuesAndComputeTotals();
        });

        vm.save = function () {
            utils.showLoading();

            vm.order.details = vm.orderDetails;

            // Update for edit
            orderService.saveOrder(0, vm.order)
                .then((response) => { toastr.success("Order saved successfully.", "Success"); }, onProcessingError)
                .finally(hideLoading);
        };

        vm.reset = function () {
            alert("Resetting!!");
        };

        vm.customerList = [];
        vm.itemList = [];

        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup()
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "name", "-- Select Customer --");
                    utils.populateDropdownlist(responses.item, vm.itemList, "codename", "-- Select Item --");
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            initialLoad();
        });

        return vm;
    };

    module.controller("addOrderController", ["customerService", "inventoryService", "orderService", "utils", "$stateParams", "$scope", "$q", addOrderController]);

})(angular.module("oisys-app"));