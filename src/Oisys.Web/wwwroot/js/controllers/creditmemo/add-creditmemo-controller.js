(function (module) {
    var addCreditMemoController = function (customerService, orderService, creditMemoService, utils, $stateParams, $scope, $q, CreditMemo, CreditMemoDetail, modelTransformer) {
        var vm = this;
        vm.creditMemo = new CreditMemo();

        // Lists
        vm.customerList = [];
        vm.orderList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;
        vm.addDetailsDisabled = true;
        vm.label = "Input new credit memo details.";

        // Public methods
        vm.addCreditMemoDetail = function () {
            var detail = new CreditMemoDetail();
            vm.creditMemo.details.splice(0, 0, detail);
        };

        vm.save = function () {
            vm.isSaving = true;

            creditMemoService.saveCreditMemo($stateParams.id, vm.creditMemo)
                .then(onSaveSuccess, utils.onError)
                .finally(onSaveComplete);
        };

        vm.deleteDetail = function (detail) {
            if (confirm("Are you sure you want to delete this credit memo item?")) {
                if (detail.id != 0) {
                    detail.isDeleted = true;
                    vm.addCreditMemoForm.$setDirty();
                } else {
                    var idx = vm.creditMemo.details.indexOf(detail);
                    vm.creditMemo.details.splice(idx, 1);
                }
            }
        };

        // Watchers
        $scope.$watch(function () {
            return vm.creditMemo;
        }, function (newVal, oldVal) {
            vm.creditMemo.update();
        }, true);

        $scope.$watch(function () {
            return vm.creditMemo.customerId;
        }, function (newVal, oldVal) {
            if (newVal && newVal != 0) {
                if (vm.creditMemo.id == 0) {
                    vm.creditMemo.clearDetails();
                }
                
                fetchOrders(newVal);
                vm.addDetailsDisabled = false;
            }
        }, true)

        // Private methods
        var resetForm = function () {
            vm.addCreditMemoForm.$setPristine();
            vm.defaultFocus = true;
        };

        var onSaveSuccess = function (response) {
            utils.showSuccessMessage("Credit memo saved successfully.");

            if ($stateParams.id == 0) {
                vm.creditMemo = new CreditMemo();
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

        var processCreditMemo = function (response) {
            var creditMemoDetails = modelTransformer.transform(response.data.details, CreditMemoDetail);
            creditMemoDetails.forEach(function (elem) {
                var idx = vm.orderList.map((element) => element.id).indexOf(elem.orderId);
                elem.selectedOrder = elem.orderDetail;
            });

            response.data.date = new Date(response.data.date);

            vm.creditMemo = modelTransformer.transform(response.data, CreditMemo);
            var customerIdx = vm.customerList.map((element) => element.id).indexOf(vm.creditMemo.customerId);
            vm.creditMemo.selectedCustomer = vm.customerList[customerIdx];

            vm.creditMemo.details = creditMemoDetails;
            console.log(creditMemoDetails);
            console.log(vm.creditMemo);

            // Update the label
            vm.label = "Update credit memo # " + vm.creditMemo.code;
        };

        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.customer, vm.customerList, "", "");

            if (responses.creditMemo) {
                processCreditMemo(responses.creditMemo);
            }
        };

        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
            };

            if ($stateParams.id != 0) {
                requests.creditMemo = creditMemoService.getCreditMemo($stateParams.id);
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

    module.controller("addCreditMemoController", ["customerService", "orderService", "creditMemoService", "utils", "$stateParams", "$scope", "$q", "CreditMemo", "CreditMemoDetail", "modelTransformer", addCreditMemoController]);

})(angular.module("oisys-app"));