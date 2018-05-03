(function (module) {
    var manufactureItemController = function (inventoryService, utils, Adjustment) {
        var vm = this;
        vm.itemList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        vm.onChange = function () {
            var idx = vm.itemList.map((element) => element.id).indexOf(vm.manufacture.id);
            vm.manufacture.current = vm.itemList[idx].currentQuantity;
            vm.manufacture.actual = vm.itemList[idx].actualQuantity;
        };

        vm.save = function () {
            vm.isSaving = true;

            console.log(vm.manufacture);

            inventoryService.adjustItemQuantity(vm.manufacture.id, vm.manufacture)
                .then(onSaveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        var onSaveSuccessful = function (response) {
            toastr.success("Item manufacture saved successfully.", "Success");
            initialLoad();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        var processItemList = function (response) {
            utils.populateDropdownlist(response, vm.itemList, "", "");
            vm.itemList.splice(0, 0, { id: 0, codeName: "Select manufactured item..", currentQuantity: 0, actualQuantity: 0 });

            resetForm();
        };

        var resetForm = function () {
            vm.manufacture = new Adjustment(0, "0", "Item manufactured");
            vm.manufactureItemForm.$setPristine();
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

    module.controller("manufactureItemController", ["inventoryService", "utils", "Adjustment", manufactureItemController]);

})(angular.module("oisys-app"));