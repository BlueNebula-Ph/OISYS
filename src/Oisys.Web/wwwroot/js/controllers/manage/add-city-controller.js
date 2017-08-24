(function (module) {

    var addCityController = function (referenceService, loadingService) {
        var vm = this;

        vm.focus = true;
        vm.reference = {
            referenceTypeId: 2,
            parentReferenceId: 0
        };

        vm.save = function () {
            loadingService.showLoading();

            // TODO: update for edit
            referenceService.saveReference(0, vm.reference)
                .then(function (response) {

                    toastr.success("City saved successfully.");

                }, function (error) {

                    toastr.error("An error has occurred.");

                })
                .finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.reset = function () {

        };

        return vm;
    };

    module.controller("addCityController", ["referenceService", "loadingService", addCityController]);

})(angular.module("oisys-app"));