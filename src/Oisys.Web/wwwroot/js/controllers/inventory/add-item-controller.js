(function (module) {
    var addItemController = function ($q, $stateParams, inventoryService, categoryService, utils) {
        var vm = this;
        var defaultItem = {
            categoryId: 0,
            quantity: 1,
            walkInPrice: 0,
            nePrice: 0,
            mainPrice: 0
        };
        vm.defaultFocus = true;
        vm.item = {};

        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            inventoryService.saveItem($stateParams.id, vm.item)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function (form) {
            form.$setPristine();
        };

        // Private methods
        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Item saved successfully.");

            // If edit, update the default values
            if ($stateParams.id != 0) {
                angular.copy(vm.item, defaultItem);
            }

            resetForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        var resetForm = function () {
            angular.copy(defaultItem, vm.item);
            vm.defaultFocus = true;
            if (vm.addItemForm) {
                vm.addItemForm.$setPristine();
            }
        };

        vm.categoryList = [];
        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.category, vm.categoryList, "name", "-- Select category --");

            if (responses.item) {
                angular.copy(responses.item.data, defaultItem);
            }
            resetForm();
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

    module.controller("addItemController", ["$q", "$stateParams", "inventoryService", "categoryService", "utils", addItemController]);

})(angular.module("oisys-app"));