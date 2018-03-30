(function (module) {
    var addQuotationController = function (customerService, inventoryService, quotationService, utils, $q) {
        var vm = this;
        var newQuotationDetail = {
            itemId: 0,
            quantity: 0
        };
        var defaultQuotation = {
            date: new Date(),
            customerId: 0,
            details: [newQuotationDetail]
        };

        // Main properties
        vm.quotation = {};

        // Helper properties
        vm.overrideDefaultControls = true;
        vm.defaultFocus = true;
        vm.isExistingCustomer = true;
        vm.customerNameFocus = false;
        vm.existingCustomerFocus = false;

        // List properties
        vm.customerList = [];
        vm.itemList = [];
        vm.quotationDetails = [];

        // Public methods
        vm.toggleCustomer = function (isExisting) {
            vm.isExistingCustomer = isExisting;
            isExisting ? vm.existingCustomerFocus = true : vm.customerNameFocus = true;
        };

        vm.save = function () {
            utils.showLoading();
            //vm.saveEnabled = false;

            quotationService.saveQuotation(0, vm.quotation)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.saveAndPrint = function () {
        };

        vm.reset = function () {
        };

        vm.addDetail = function () {
            var detail = angular.copy(newQuotationDetail);
            detail.isFocused = true;
            vm.quotation.details.push(detail);
        };

        // Private methods
        var saveSuccessful = function () {
            utils.showSuccessMessage("Quotation saved successfully.");
            clearForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            //vm.saveEnabled = true;
        };

        var clearForm = function () {
            angular.copy(defaultQuotation, vm.quotation);
            vm.defaultFocus = true;

            if (vm.quotationForm) {
                vm.quotationForm.$setPristine();
                vm.quotationForm.$setUntouched();
            }
        };

        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup()
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "", "");
                    utils.populateDropdownlist(responses.item, vm.itemList, "", "");
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            initialLoad();
            clearForm();
        });

        return vm;
    };

    module.controller("addQuotationController", ["customerService", "inventoryService", "quotationService", "utils", "$q", addQuotationController]);

})(angular.module("oisys-app"));