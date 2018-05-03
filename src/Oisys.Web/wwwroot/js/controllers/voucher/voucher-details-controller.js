(function (module) {
    var cashVoucherDetailsController = function (cashVoucherService, utils, $stateParams) {
        var vm = this;

        vm.cashVoucherDetails = {};

        // Initialize the details
        $(function () {
            utils.showLoading();

            cashVoucherService.getCashVoucher($stateParams.id)
                .then(function (response) {
                    var data = response.data;
                    angular.copy(data, vm.cashVoucherDetails);
                }, function (error) {
                    console.log(error);
                })
                .finally(function () {
                    utils.hideLoading();
                });
        });

        return vm;
    };

    module.controller("cashVoucherDetailsController", ["cashVoucherService", "utils", "$stateParams", cashVoucherDetailsController]);

})(angular.module("oisys-app"));