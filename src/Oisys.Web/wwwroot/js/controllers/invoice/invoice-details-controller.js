(function (module) {
    var invoiceDetailsController = function (invoiceService, utils, $stateParams) {
        var vm = this;
        vm.invoiceInfo = {};

        var processOrder = function (response) {
            var data = response.data;
            angular.copy(data, vm.invoiceInfo);
        };

        // Initialize the details
        $(function () {
            utils.showLoading();

            invoiceService.getOrder($stateParams.id)
                .then(processOrder, utils.onError)
                .finally(utils.hideLoading);
        });

        return vm;
    };

    module.controller("invoiceDetailsController", ["invoiceService", "utils", "$stateParams", invoiceDetailsController]);

})(angular.module("oisys-app"));