(function (module) {
    var addItemController = function (inventoryService, referenceService, loadingService, $q) {
        var vm = this;
        vm.defaultFocus = true;
        vm.item = {
            categoryId: 0,
            quantity: 1,
            walkInPrice: 0,
            nePrice: 0,
            mainPrice: 0
        };

        vm.save = function () {
            loadingService.showLoading();

            inventoryService.saveItem(0, vm.item)
                .then((response) => { toastr.success("Item saved successfully.") }, onProcessingError)
                .finally(hideLoading);
        };

        vm.reset = function (form) {
            form.$setPristine();
        };

        vm.categoryList = [];
        var processCategories = function (response) {
            angular.copy(response.data, vm.categoryList);
            vm.categoryList.splice(0, 0, { id: 0, code: "-- Select Category --" });
        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        var onProcessingError = function (error) {
            toastr.error("There was an error processing your request.", "Error");
            console.log(error);
        };

        var initialLoad = function () {
            loadingService.showLoading();

            var requests = {
                category: referenceService.getReferenceLookup(1)
            };

            $q.all(requests)
                .then((responses) => {
                    processCategories(responses.category);
                }, onProcessingError)
                .finally(hideLoading);
        };

        $(function () {
            initialLoad();
        });

        return vm;
    };

    module.controller("addItemController", ["inventoryService", "referenceService", "loadingService", "$q", addItemController]);

})(angular.module("oisys-app"));