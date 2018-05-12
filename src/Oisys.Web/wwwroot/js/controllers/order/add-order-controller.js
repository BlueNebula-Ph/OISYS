(function (module) {
    var addOrderController = function (customerService, inventoryService, orderService, utils, $stateParams, $scope, $q, Order, OrderDetail, modelTransformer) {
        var vm = this;
        vm.order = new Order();

        // Lists
        vm.customerList = [];
        vm.itemList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;
        vm.label = "Input details for new order.";

        // Public methods
        vm.addOrderDetail = function () {
            var detail = new OrderDetail();
            vm.order.details.splice(0, 0, detail);
        };

        vm.save = function () {
            vm.isSaving = true;

            orderService.saveOrder($stateParams.id, vm.order)
                .then(onSaveSuccess, utils.onError)
                .finally(onSaveComplete);
        };

        vm.deleteDetail = function (detail) {
            if (confirm("Are you sure you want to delete this order detail?")) {
                if (detail.id != 0) {
                    detail.isDeleted = true;
                    vm.addOrderForm.$setDirty();
                } else {
                    var idx = vm.order.details.indexOf(detail);
                    vm.order.details.splice(idx, 1);
                }
            }
        };

        // Watchers
        $scope.$watch(function () {
            return vm.order;
        }, function (newVal, oldVal) {
            vm.order.update();
        }, true);

        // Private methods
        var resetForm = function () {
            vm.addOrderForm.$setPristine();
            vm.defaultFocus = true;
        };

        var onSaveSuccess = function (response) {
            utils.showSuccessMessage("Order saved successfully.");

            if ($stateParams.id == 0) {
                vm.order = new Order();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        var processOrder = function (response) {
            var orderDetails = modelTransformer.transform(response.data.details, OrderDetail);
            orderDetails.forEach(function (elem) {
                var idx = vm.itemList.map((element) => element.id).indexOf(elem.itemId);
                elem.selectedItem = vm.itemList[idx];
            });

            response.data.date = new Date(response.data.date);
            response.data.dueDate = new Date(response.data.dueDate);

            vm.order = modelTransformer.transform(response.data, Order);
            var customerIdx = vm.customerList.map((element) => element.id).indexOf(vm.order.customerId);
            vm.order.selectedCustomer = vm.customerList[customerIdx];

            vm.order.details = orderDetails;

            // Update the label
            vm.label = "Update details for Order # " + vm.order.code;
        };

        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.customer, vm.customerList, "", "");
            utils.populateDropdownlist(responses.item, vm.itemList, "", "");

            if (responses.order) {
                processOrder(responses.order);
            }
        };
        
        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup()
            };

            if ($stateParams.id != 0) {
                requests.order = orderService.getOrder($stateParams.id);
            }

            $q.all(requests)
                .then(processResponses, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            initialLoad();
        });

        return vm;
    };

    module.controller("addOrderController", ["customerService", "inventoryService", "orderService", "utils", "$stateParams", "$scope", "$q", "Order", "OrderDetail", "modelTransformer", addOrderController]);

})(angular.module("oisys-app"));