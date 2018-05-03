(function (module) {
    var addProvinceController = function ($stateParams, provinceService, utils, Province, City, modelTransformer) {
        var vm = this;
        vm.province = new Province();

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        // Public methods
        vm.save = function () {
            vm.isSaving = true;

            provinceService.saveProvince($stateParams.id, vm.province)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.addCity = function () {
            var newCity = new City();
            vm.province.cities.push(newCity);
        };

        vm.removeCity = function (city) {
            if (confirm("Are you sure you want to delete this city?")) {
                if (city.id != 0) {
                    city.isDeleted = true;
                    vm.addProvinceForm.$setDirty();
                } else {
                    var idx = vm.province.cities.indexOf(city);
                    vm.province.cities.splice(idx, 1);
                }
            }
        };

        // Private methods
        var resetForm = function () {
            vm.addProvinceForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Province saved successfully.");

            if ($stateParams.id == 0) {
                vm.province = new Province();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        // Load
        var processProvince = function (response) {
            vm.province = modelTransformer.transform(response.data, Province);
            resetForm();
        };

        var loadProvince = function () {
            if ($stateParams.id != 0) {
                utils.showLoading();

                provinceService.getProvinceById($stateParams.id)
                    .then(processProvince, utils.onError)
                    .finally(utils.hideLoading);
            }
        };

        $(function () {
            loadProvince();
        });

        return vm;
    };

    module.controller("addProvinceController", ["$stateParams", "provinceService", "utils", "Province", "City", "modelTransformer", addProvinceController]);

})(angular.module("oisys-app"));