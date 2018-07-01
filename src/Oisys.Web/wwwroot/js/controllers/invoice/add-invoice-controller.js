(function (module) {
    var addInvoiceController = function (customerService, orderService, invoiceService, utils, $stateParams, $scope, $q, Invoice, InvoiceDetail, modelTransformer) {
        var vm = this;
        vm.invoice = new Invoice();

        // Lists
        vm.customerList = [];
        vm.ordersForInvoicing = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;
        vm.label = "Input details for new invoice.";

        // Public methods
        vm.addInvoiceDetail = function () {
            var detail = new InvoiceDetail();
            vm.invoice.details.splice(0, 0, detail);
        };

        vm.save = function () {
            vm.isSaving = true;

            invoiceService.saveInvoice($stateParams.id, vm.invoice)
                .then(onSaveSuccess, utils.onError)
                .finally(onSaveComplete);
        };

        vm.deleteDetail = function (detail) {
            if (confirm("Are you sure you want to delete this invoice detail?")) {
                if (detail.id != 0) {
                    detail.isDeleted = true;
                    vm.addInvoiceForm.$setDirty();
                } else {
                    var idx = vm.invoice.details.indexOf(detail);
                    vm.invoice.details.splice(idx, 1);
                }
            }
        };

        // Watchers
        $scope.$watch(function () {
            return vm.invoice;
        }, function (newVal, oldVal) {
            vm.invoice.update();
        }, true);

        $scope.$watch(function () {
            return vm.invoice.selectedCustomer;
        }, function (newVal, oldVal) {
            if (newVal && newVal != 0) {
                utils.showLoading();
                orderService
                    .getOrdersForInvoicing(newVal.id)
                    .then(processOrders, utils.onError)
                    .finally(utils.hideLoading);
            }
        });

        // Private methods
        var resetForm = function () {
            vm.addInvoiceForm.$setPristine();
            vm.defaultFocus = true;
        };

        var onSaveSuccess = function (response) {
            utils.showSuccessMessage("Invoice saved successfully.");

            if ($stateParams.id == 0) {
                vm.invoice = new Invoice();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };
        
        var processOrders = function (response) {
            console.log(response.data);
            angular.copy(response.data, vm.ordersForInvoicing);
        };

        var processInvoice = function (response) {
            var invoiceDetails = modelTransformer.transform(response.data.details, InvoiceDetail);
            invoiceDetails.forEach(function (elem) {
                var idx = vm.itemList.map((element) => element.id).indexOf(elem.itemId);
                elem.selectedItem = vm.itemList[idx];
            });

            response.data.date = new Date(response.data.date);

            vm.invoice = modelTransformer.transform(response.data, Invoice);
            var customerIdx = vm.customerList.map((element) => element.id).indexOf(vm.invoice.customerId);
            vm.invoice.selectedCustomer = vm.customerList[customerIdx];

            vm.invoice.details = invoiceDetails;

            // Update the label
            vm.label = "Update details for Invoice # " + vm.invoice.invoiceNumber;
        };

        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.customer, vm.customerList, "", "");

            if (responses.invoice) {
                processInvoice(responses.invoice);
            }
        };

        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
            };

            if ($stateParams.id != 0) {
                requests.invoice = invoiceService.getInvoice($stateParams.id);
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

    module.controller("addInvoiceController", ["customerService", "orderService", "invoiceService", "utils", "$stateParams", "$scope", "$q", "Invoice", "InvoiceDetail", "modelTransformer", addInvoiceController]);

})(angular.module("oisys-app"));