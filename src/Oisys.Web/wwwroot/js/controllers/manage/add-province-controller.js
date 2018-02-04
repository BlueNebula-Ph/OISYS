(function (module) {
    var addProvinceController = function ($stateParams, provinceService, utils) {
        var vm = this;

        // Data
        var defaultProvince = {};
        vm.province = {
            name: "",
            cities: []
        };

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            provinceService.saveProvince($stateParams.id, vm.province)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        vm.addCity = function () {
            var newCity = { id: 0, name: "", focus: true };
            vm.province.cities.push(newSub);
        };

        vm.removeCity = function ($index) {
            var city = vm.province.cities[$index];
            if (city && city.id != 0) {
                if (!confirm("This city has already been saved. Are you sure?")) {
                    return;
                }

                provinceService
                    .deleteCity($stateParams.id, city.id)
                    .then(() => {
                        vm.province.cities.splice($index, 1);
                        loadProvince();
                    }, utils.onError);
            } else {
                vm.province.cities.splice($index, 1);
            }
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultProvince, vm.province);
            vm.addProvinceForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Province saved successfully.");

            if ($stateParams.id == 0) {
                clearForm();
            } else {
                loadProvince();
            }
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        // Load
        var processProvince = function (response) {
            angular.copy(response.data, defaultProvince);
            clearForm();
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

    module.controller("addProvinceController", ["$stateParams", "provinceService", "utils", addProvinceController]);

})(angular.module("oisys-app"));