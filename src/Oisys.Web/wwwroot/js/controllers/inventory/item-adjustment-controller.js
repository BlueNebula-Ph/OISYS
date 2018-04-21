(function (module) {
    var itemAdjustmentController = function (inventoryService, utils, Adjustment) {
        var vm = this;
        vm.itemList = [];
        vm.adjustment = new Adjustment();

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        vm.onChange = function () {
            var idx = vm.itemList.map((element) => element.id).indexOf(vm.adjustment.id);
            vm.adjustment.current = vm.itemList[idx].currentQuantity;
            vm.adjustment.actual = vm.itemList[idx].actualQuantity;
        };

        vm.save = function () {
            vm.isSaving = true;

            inventoryService.adjustItemQuantity(vm.adjustment.id, vm.adjustment)
                .then(onSaveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        var onSaveSuccessful = function (response) {
            toastr.success("Adjustment saved successfully.", "Success");
            initialLoad();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };
        
        var processItemList = function (response) {
            utils.populateDropdownlist(response, vm.itemList, "", "");
            vm.itemList.splice(0, 0, { id: 0, codeName: "Select an item to adjust..", currentQuantity: 0, actualQuantity: 0 });

            resetForm();
        };

        var resetForm = function () {
            vm.adjustment = new Adjustment();
            vm.addAdjustmentForm.$setPristine();
            vm.defaultFocus = true;
        };

        var initialLoad = function () {
            utils.showLoading();

            inventoryService.getItemLookup()
                .then(processItemList, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            initialLoad();
        });

        return vm;
    };

    module.controller("itemAdjustmentController", ["inventoryService", "utils", "Adjustment", itemAdjustmentController]);

})(angular.module("oisys-app"));