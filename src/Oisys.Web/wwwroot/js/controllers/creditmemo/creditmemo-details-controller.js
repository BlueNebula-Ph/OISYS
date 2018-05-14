(function (module) {
    var creditMemoDetailsController = function (creditMemoService, utils, $stateParams) {
        var vm = this;
        vm.creditMemo = {};

        var processOrder = function (response) {
            var data = response.data;
            angular.copy(data, vm.creditMemo);
        };

        // Initialize the details
        $(function () {
            utils.showLoading();

            creditMemoService.getCreditMemo($stateParams.id)
                .then(processOrder, utils.onError)
                .finally(utils.hideLoading);
        });

        return vm;
    };

    module.controller("creditMemoDetailsController", ["creditMemoService", "utils", "$stateParams", creditMemoDetailsController]);

})(angular.module("oisys-app"));