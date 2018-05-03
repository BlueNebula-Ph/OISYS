(function (module) {
    var itemDetailsController = function (inventoryService, utils, $stateParams) {
        var vm = this;

        vm.itemDetails = {};

        // Initialize the details
        $(function () {
            utils.showLoading();

            inventoryService.getItem($stateParams.id)
                .then(function (response) {
                    var data = response.data;
                    angular.copy(data, vm.itemDetails);
                }, function (error) {
                    console.log(error);
                })
                .finally(function () {
                    utils.hideLoading();
                });
        });

        return vm;
    };

    module.controller("itemDetailsController", ["inventoryService", "utils", "$stateParams", itemDetailsController]);

})(angular.module("oisys-app"));