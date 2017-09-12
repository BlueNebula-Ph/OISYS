(function (module) {

    var customerDetailsController = function (customerService, loadingService, $stateParams) {
        var vm = this;

        vm.customerDetails = {};

        // Initialize the details
        $(function () {
            loadingService.showLoading();

            customerService.getCustomer($stateParams.id)
                .then(function (response) {
                    var data = response.data;
                    angular.copy(data, vm.customerDetails);
                }, function (error) {
                    console.log(error);
                })
                .finally(function () {
                    loadingService.hideLoading();
                });
        });

        return vm;
    };

    module.controller("customerDetailsController", ["customerService", "loadingService", "$stateParams", customerDetailsController]);

})(angular.module("oisys-app"));