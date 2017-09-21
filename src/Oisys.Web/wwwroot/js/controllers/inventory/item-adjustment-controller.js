(function (module) {
    var itemAdjustmentController = function (inventoryService, loadingService, $q, $scope) {
        var vm = this;

        vm.adjustment = {
            id: 0,
            adjustmentType: "-1"
        };

        vm.quantities = {
            current: 0,
            actual: 0
        };

        vm.onChange = function () {
            var idx = vm.itemList.map((element) => element.id).indexOf(vm.adjustment.id);
            vm.quantities.current = vm.itemList[idx].currentQuantity;
            vm.quantities.actual = vm.itemList[idx].actualQuantity;
        };

        vm.save = function () {
            loadingService.showLoading();

            inventoryService.adjustItemQuantity(vm.adjustment.id, vm.adjustment)
                .then((response) => { toastr.success("Adjustment successfully processed.", "Success"); }, onProcessingError)
                .finally(hideLoading);
        };

        vm.reset = function () {

        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        vm.itemList = [];
        var processItemList = function (response) {
            angular.copy(response.data, vm.itemList);
            vm.itemList.splice(0, 0, { id: 0, codeName: "Select an item to adjust..", currentQuantity: 0, actualQuantity: 0 });
        };

        var onProcessingError = function (error) {
            toastr.error("There was an error processing your request.", "Error");
            console.log(error);
        };

        var initialLoad = function () {
            loadingService.showLoading();

            var requests = {
                item: inventoryService.getItemLookup()
            };

            $q.all(requests)
                .then((responses) => {
                    processItemList(responses.item);
                }, onProcessingError)
                .finally(hideLoading);
        };

        $(function () {
            initialLoad();
        });

        return vm;
    };

    module.controller("itemAdjustmentController", ["inventoryService", "loadingService", "$q", "$scope", itemAdjustmentController]);

})(angular.module("oisys-app"));