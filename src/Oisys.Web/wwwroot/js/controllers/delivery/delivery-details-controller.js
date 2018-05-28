(function (module) {
    var deliveryDetailsController = function (deliveryService, utils, $stateParams) {
        var vm = this;
        vm.deliveryInfo = {};

        var processDelivery = function (response) {
            var data = response.data;
            angular.copy(data, vm.deliveryInfo);
        };

        // Initialize the details
        $(function () {
            utils.showLoading();

            deliveryService.getDelivery($stateParams.id)
                .then(processDelivery, utils.onError)
                .finally(utils.hideLoading);
        });

        return vm;
    };

    module.controller("deliveryDetailsController", ["deliveryService", "utils", "$stateParams", deliveryDetailsController]);
})(angular.module("oisys-app"));