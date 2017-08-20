(function (module) {

    var addCategoryController = function (referenceService, loadingService) {
        var vm = this;
        var referenceType = 1;

        vm.focus = true;
        vm.reference = {
            referenceTypeId: 1
        };

        vm.save = function () {
            loadingService.showLoading();

            referenceService.saveReference(0, vm.reference)
                .then(function () {
                    toastr.success("Category saved successfully.");
                }, function (error) {
                    toastr.error("An error has occurred.");
                })
                .finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.reset = function () {
            addCategoryForm.$setPristine();
        };

        return vm;
    };

    module.controller("addCategoryController", ["referenceService", "loadingService", addCategoryController]);

})(angular.module("oisys-app"));