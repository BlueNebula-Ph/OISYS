(function (module) {
    var addCategoryController = function ($stateParams, categoryService, utils) {
        var vm = this;

        // Data
        var defaultCategory = {};
        vm.category = {};

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            categoryService.saveCategory($stateParams.id, vm.category)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultCategory, vm.category);
            vm.addCategoryForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Category saved successfully.");

            // If edit, update the default values
            if ($stateParams.id != 0) {
                angular.copy(vm.category, defaultCategory);
            }

            clearForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        // Load
        var processCategory = function (response) {
            angular.copy(response.data, defaultCategory);
            clearForm();
        };

        var loadCategory = function () {
            if ($stateParams.id != 0) {
                utils.showLoading();

                categoryService.getCategory($stateParams.id)
                    .then(processCategory, utils.onError)
                    .finally(utils.hideLoading);
            }
        };

        $(function () {
            loadCategory();
        });

        return vm;
    };

    module.controller("addCategoryController", ["$stateParams", "categoryService", "utils", addCategoryController]);

})(angular.module("oisys-app"));