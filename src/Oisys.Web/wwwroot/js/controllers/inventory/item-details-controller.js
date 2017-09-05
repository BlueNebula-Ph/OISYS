(function (module) {

    var itemDetailsController = function (inventoryService, loadingService) {
        var vm = this;

        vm.itemDetails = {};

        // Initialize the details
        $(function () {
            loadingService.showLoading();

            inventoryService.getItem(1)
                .then(function (response) {
                    var data = response.data;
                    angular.copy(data, vm.itemDetails);
                }, function (error) {
                    console.log(error);
                })
                .finally(function () {
                    loadingService.hideLoading();
                });
        });

        return vm;
    };

    module.controller("itemDetailsController", ["inventoryService", "loadingService", itemDetailsController]);

})(angular.module("oisys-app"));