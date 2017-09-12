(function (module) {

    var addOrderController = function (customerService, inventoryService, orderService, loadingService, $stateParams, $scope) {
        var vm = this;

        vm.defaultFocus = true;
        vm.order = {
            customerId: 0
        };
        vm.newOrderDetail = {
            id: 0,
            orderId: 0,
            itemId: 0,
            quantity: 0,
            selectedItem: { id: 0, codeName: "-- Select Item --" },
            price: 0,
            totalPrice: 0,
            focus: true
        };

        vm.orderDetails = [];
        vm.addOrderDetail = function () {
            var detail = angular.copy(vm.newOrderDetail);
            vm.orderDetails.splice(0, 0, detail);
        };

        $scope.$watchCollection(function () {
            var collectionToWatch = [];
            var totalOrderDetails = vm.orderDetails.length;
            for (var i = 0; i < totalOrderDetails; i++) {
                collectionToWatch.push(vm.orderDetails[i].selectedItem);
            }
            return collectionToWatch;
        }, function (newValue, oldValue) {
            var total = newValue.length;
            for (var i = 0; i < total; i++) {
                vm.orderDetails[i].unit = newValue[i].unit;
                vm.orderDetails[i].price = newValue[i].mainPrice;
            }
        });

        vm.save = function () {
            alert("SAVING!");
        };

        vm.reset = function () {
            alert("REsetting!!");
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
                    //vm.itemList.splice(0, 0, { id: 0, codeName: "-- Select Item --" });
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