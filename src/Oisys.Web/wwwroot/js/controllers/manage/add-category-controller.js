(function (module) {
    var addCategoryController = function ($stateParams, categoryService, utils, Category, modelTransformer) {
        var vm = this;
        vm.category = new Category();

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        // Public methods
        vm.save = function () {
            vm.isSaving = true;

            categoryService.saveCategory($stateParams.id, vm.category)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        // Private methods
        var resetForm = function () {
            vm.addCategoryForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Category saved successfully.");

            // If new, clear the form
            if ($stateParams.id == 0) {
                vm.category = new Category();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        // Load
        var processCategory = function (response) {
            vm.category = modelTransformer.transform(response.data, Category);
            resetForm();
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

    module.controller("addCategoryController", ["$stateParams", "categoryService", "utils", "Category", "modelTransformer", addCategoryController]);

})(angular.module("oisys-app"));