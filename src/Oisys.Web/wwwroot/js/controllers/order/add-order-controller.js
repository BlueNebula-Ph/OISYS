(function (module) {

    var addOrderController = function (customerService, inventoryService, orderService, loadingService, $stateParams, $scope) {
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
            loadingService.showLoading();

            vm.order.details = vm.orderDetails;

            // Update for edit
            orderService.saveOrder(0, vm.order)
                .then(function (response) {
                    toastr.success("Order saved successfully.", "Success");
                }, function (error) {
                    toastr.error("An error has occurred.", "Error");
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.reset = function () {
            alert("Resetting!!");
        };

        vm.customerList = [];
        var loadCustomers = function () {
            loadingService.showLoading();

            customerService.getCustomerLookup()
                .then(function (response) {
                    var data = response.data;
                    angular.copy(data, vm.customerList);
                    vm.customerList.splice(0, 0, { id: 0, name: "-- Select Customer --" });
                }, function (error) {
                    console.log(error);
                    toastr.error("There was an error processing your request.", "Error");
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.itemList = [];
        var loadItems = function () {
            loadingService.showLoading();

            inventoryService.getItemLookup()
                .then(function (response) {
                    var data = response.data;
                    angular.copy(data, vm.itemList);
                }, function (error) {
                    console.log(error);
                    toastr.error("There was an error processing your request.", "Error");
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        $(function () {
            loadCustomers();
            loadItems();
        });

        return vm;
    };

    module.controller("addOrderController", ["customerService", "inventoryService", "orderService", "loadingService", "$stateParams", "$scope", addOrderController]);

})(angular.module("oisys-app"));