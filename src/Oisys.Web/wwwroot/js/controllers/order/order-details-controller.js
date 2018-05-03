(function (module) {
    var orderDetailsController = function (orderService, utils, $stateParams) {
        var vm = this;
        vm.orderInfo = {};

        var processOrder = function (response) {
            var data = response.data;
            angular.copy(data, vm.orderInfo);
        };

        // Initialize the details
        $(function () {
            utils.showLoading();

            orderService.getOrder($stateParams.id)
                .then(processOrder, utils.onError)
                .finally(utils.hideLoading);
        });

        return vm;
    };

    module.controller("orderDetailsController", ["orderService", "utils", "$stateParams", orderDetailsController]);

})(angular.module("oisys-app"));