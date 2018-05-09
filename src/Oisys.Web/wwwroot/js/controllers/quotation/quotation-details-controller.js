(function (module) {
    var quotationDetailsController = function (quotationService, utils, $stateParams) {
        var vm = this;
        vm.quotationInfo = {};

        var processOrder = function (response) {
            var data = response.data;
            angular.copy(data, vm.quotationInfo);
        };

        // Initialize the details
        $(function () {
            utils.showLoading();

            quotationService.getQuotation($stateParams.id)
                .then(processOrder, utils.onError)
                .finally(utils.hideLoading);
        });

        return vm;
    };

    module.controller("quotationDetailsController", ["quotationService", "utils", "$stateParams", quotationDetailsController]);

})(angular.module("oisys-app"));