(function (module) {
    var addQuotationController = function ($scope, $stateParams, $q, customerService, inventoryService, quotationService, utils, Quotation, QuotationDetail, modelTransformer) {
        var vm = this;
        vm.quotation = new Quotation();

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        // List properties
        vm.customerList = [];
        vm.itemList = [];
        vm.quotationDetails = [];

        // Public methods
        vm.save = function () {
            vm.isSaving = true;

            quotationService.saveQuotation($stateParams.id, vm.quotation)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.addDetail = function () {
            var detail = new QuotationDetail();
            vm.quotation.details.splice(0, 0, detail);
        };

        vm.deleteDetail = function (detail) {
            if (confirm("Are you sure you want to delete this quotation detail?")) {
                if (detail.id != 0) {
                    detail.isDeleted = true;
                    vm.addQuotationForm.$setDirty();
                } else {
                    var idx = vm.quotation.details.indexOf(detail);
                    vm.quotation.details.splice(idx, 1);
                }
            }
        };

        // Watchers
        $scope.$watch(function () {
            return vm.quotation;
        }, function (newVal, oldVal) {
            vm.quotation.update();
        }, true);

        // Private methods
        var resetForm = function () {
            vm.addQuotationForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function () {
            utils.showSuccessMessage("Quotation saved successfully.");

            if ($stateParams.id == 0) {
                vm.quotation = new Quotation();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        var processQuotation = function (response) {
            var quotationDetails = modelTransformer.transform(response.data.details, QuotationDetail);
            quotationDetails.forEach(function (elem) {
                var idx = vm.itemList.map((element) => element.id).indexOf(elem.itemId);
                elem.selectedItem = vm.itemList[idx];
            });

            response.data.date = new Date(response.data.date);

            vm.quotation = modelTransformer.transform(response.data, Quotation);
            var customerIdx = vm.customerList.map((element) => element.id).indexOf(vm.quotation.customerId);
            vm.quotation.selectedCustomer = vm.customerList[customerIdx];

            vm.quotation.details = quotationDetails;
        };

        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.customer, vm.customerList, "", "");
            utils.populateDropdownlist(responses.item, vm.itemList, "", "");

            if (responses.quotation) {
                processQuotation(responses.quotation);
            }
        };
       
        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup()
            };

            if ($stateParams.id != 0) {
                requests.quotation = quotationService.getQuotation($stateParams.id);
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

    module.controller("addQuotationController", ["$scope", "$stateParams", "$q", "customerService", "inventoryService", "quotationService", "utils", "Quotation", "QuotationDetail", "modelTransformer", addQuotationController]);

})(angular.module("oisys-app"));