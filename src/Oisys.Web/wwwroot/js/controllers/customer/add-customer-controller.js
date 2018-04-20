(function (module) {
    var addCustomerController = function ($scope, $q, $stateParams, customerService, provinceService, utils, Customer, Reference, modelTransformer) {
        var vm = this;
        vm.customer = new Customer();

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        // Lists
        vm.provinceList = [];
        vm.citiesList = [];
        vm.priceList = Reference.pricelist;

        vm.save = function () {
            vm.isSaving = true;

            customerService.saveCustomer($stateParams.id, vm.customer)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        // Watchers
        $scope.$watch(function () {
            return vm.customer.provinceId;
        }, function (newVal, oldVal) {
            var selectedProvince = vm.provinceList.find(function (elem) { return elem.id == newVal; });

            if (selectedProvince) {
                vm.citiesList = selectedProvince.cities;
            }
        }, true);

        // Private methods
        var resetForm = function () {
            vm.addCustomerForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Customer saved successfully.");

            // If new, clear the form
            if ($stateParams.id == 0) {
                vm.customer = new Customer();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        // Load
        var processCustomer = function (response) {
            vm.customer = modelTransformer.transform(response.data, Customer);
            resetForm();
        };

        var initialLoad = function () {
            utils.showLoading();

            var requests = {
                province: provinceService.getProvinceLookup()
            };

            if ($stateParams.id != 0) {
                requests.customer = customerService.getCustomer($stateParams.id);
            }

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.province, vm.provinceList, "", "");

                    if (responses.customer) {
                        processCustomer(responses.customer);
                    }
                    
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            initialLoad();
        });

        return vm;
    };

    module.controller("addCustomerController", ["$scope", "$q", "$stateParams", "customerService", "provinceService", "utils", "Customer", "Reference", "modelTransformer", addCustomerController]);

})(angular.module("oisys-app"));