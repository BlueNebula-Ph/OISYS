(function (module) {
    var addItemController = function ($q, $stateParams, inventoryService, categoryService, utils, Item, modelTransformer) {
        var vm = this;
        vm.item = new Item();

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        // Lists
        vm.categoryList = [];

        vm.save = function () {
            vm.isSaving = true;

            inventoryService.saveItem($stateParams.id, vm.item)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        // Private methods
        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Item saved successfully.");

            // If edit, update the default values
            if ($stateParams.id == 0) {
                vm.item = new Item();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        var resetForm = function () {
            vm.addItemForm.$setPristine();
            vm.defaultFocus = true;
        };

        var processItem = function (response) {
            vm.item = modelTransformer.transform(response.data, Item);
            vm.item.setQuantity();
            resetForm();
        }
        
        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.category, vm.categoryList, "", "");

            if (responses.item) {
                processItem(responses.item);
            }
        };

        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                category: categoryService.getCategoryLookup(),
            };

            if ($stateParams.id != 0) {
                requests.item = inventoryService.getItem($stateParams.id);
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

    module.controller("addItemController", ["$q", "$stateParams", "inventoryService", "categoryService", "utils", "Item", "modelTransformer", addItemController]);

})(angular.module("oisys-app"));