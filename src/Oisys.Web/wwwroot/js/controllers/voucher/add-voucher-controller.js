(function (module) {
    var addCashVoucherController = function ($q, $stateParams, cashVoucherService, referenceService, utils, CashVoucher, modelTransformer) {
        var vm = this;
        vm.cashVoucher = new CashVoucher();

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        // Lists
        vm.categoryList = [];

        vm.save = function () {
            vm.isSaving = true;

            cashVoucherService.saveCashVoucher($stateParams.id, vm.cashVoucher)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        // Private methods
        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Cash voucher saved successfully.");

            // If edit, update the default values
            if ($stateParams.id == 0) {
                vm.cashVoucher = new CashVoucher();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        var resetForm = function () {
            vm.addVoucherForm.$setPristine();
            vm.defaultFocus = true;
        };

        var processCashVoucher = function (response) {
            vm.cashVoucher = modelTransformer.transform(response.data, CashVoucher);
            resetForm();
        }

        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.category, vm.categoryList, "", "");

            if (responses.cashVoucher) {
                processCashVoucher(responses.cashVoucher);
            }
        };

        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                category: referenceService.getReferenceLookup(2),
            };

            if ($stateParams.id != 0) {
                requests.cashVoucher = cashVoucherService.getCashVoucher($stateParams.id);
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

    module.controller("addCashVoucherController", ["$q", "$stateParams", "cashVoucherService", "referenceService", "utils", "CashVoucher", "modelTransformer", addCashVoucherController]);

})(angular.module("oisys-app"));