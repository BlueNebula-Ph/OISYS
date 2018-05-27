(function (module) {
    var addDeliveryController = function (customerService, orderService, deliveryService, utils, $stateParams, $scope, $q, Delivery, DeliveryDetail, modelTransformer) {
        var vm = this;
        vm.delivery = new Delivery();

        // Lists
        vm.customerList = [];
        vm.orderList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;
        vm.addDetailsDisabled = true;
        vm.label = "Input new delivery details.";

        // Public methods
        vm.addDeliveryDetail = function () {
            var detail = new DeliveryDetail();
            vm.delivery.details.splice(0, 0, detail);
        };

        vm.save = function () {
            vm.isSaving = true;

            deliveryService.saveDelivery($stateParams.id, vm.delivery)
                .then(onSaveSuccess, utils.onError)
                .finally(onSaveComplete);
        };

        vm.deleteDetail = function (detail) {
            if (confirm("Are you sure you want to delete this delivery item?")) {
                if (detail.id != 0) {
                    detail.isDeleted = true;
                    vm.addDeliveryForm.$setDirty();
                } else {
                    var idx = vm.delivery.details.indexOf(detail);
                    vm.delivery.details.splice(idx, 1);
                }
            }
        };

        // Watchers
        $scope.$watch(function () {
            return vm.delivery;
        }, function (newVal, oldVal) {
            vm.delivery.update();
        }, true);

        $scope.$watch(function () {
            return vm.delivery.customerId;
        }, function (newVal, oldVal) {
            if (newVal && newVal != 0) {
                if (vm.delivery.id == 0) {
                    vm.delivery.clearDetails();
                }

                fetchOrders(newVal);
                vm.addDetailsDisabled = false;
            }
        }, true)

        // Private methods
        var resetForm = function () {
            vm.addDeliveryForm.$setPristine();
            vm.defaultFocus = true;
        };

        var onSaveSuccess = function (response) {
            utils.showSuccessMessage("Delivery saved successfully.");

            if ($stateParams.id == 0) {
                vm.delivery = new Delivery();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        var processOrders = function (response) {
            angular.copy(response.data, vm.orderList);
        };

        var fetchOrders = function (customerId) {
            utils.showLoading();
            orderService.getOrderLookup(customerId)
                .then(processOrders, utils.onError)
                .finally(utils.hideLoading);
        };

        var processDelivery = function (response) {
            var deliveryDetails = modelTransformer.transform(response.data.details, DeliveryDetail);
            deliveryDetails.forEach(function (elem) {
                var idx = vm.orderList.map((element) => element.id).indexOf(elem.orderId);
                elem.selectedOrder = elem.orderDetail;
            });

            response.data.date = new Date(response.data.date);

            vm.delivery = modelTransformer.transform(response.data, Delivery);
            var customerIdx = vm.customerList.map((element) => element.id).indexOf(vm.delivery.customerId);
            vm.delivery.selectedCustomer = vm.customerList[customerIdx];

            vm.delivery.details = deliveryDetails;

            // Update the label
            vm.label = "Update delivery # " + vm.delivery.code;
        };

        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.customer, vm.customerList, "", "");

            if (responses.delivery) {
                processDelivery(responses.delivery);
            }
        };

        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
            };

            if ($stateParams.id != 0) {
                requests.delivery = deliveryService.getDelivery($stateParams.id);
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

    module.controller("addDeliveryController", ["customerService", "orderService", "deliveryService", "utils", "$stateParams", "$scope", "$q", "Delivery", "DeliveryDetail", "modelTransformer", addDeliveryController]);

})(angular.module("oisys-app"));