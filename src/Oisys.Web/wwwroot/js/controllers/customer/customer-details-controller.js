(function (module) {

    var customerDetailsController = function (customerService, loadingService) {
        var vm = this;

        vm.customerDetails = {};

        // Initialize the details
        $(function () {
            loadingService.showLoading();

            customerService.getCustomer(1)
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

    module.controller("customerDetailsController", ["customerService", "loadingService", customerDetailsController]);

})(angular.module("oisys-app"));