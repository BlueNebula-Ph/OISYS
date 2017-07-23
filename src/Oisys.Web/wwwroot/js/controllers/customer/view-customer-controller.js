(function () {
    var viewCustomerController = function (customerService, loadingService) {
        var vm = this;

        vm.customers = [];

        $(function () {
            loadingService.showLoading();

            customerService.fetchAllCustomers()
                .then(function (response) {
                    vm.customers = response.data;
                }, function (error) {
                    console.log(error);
                }).finally(function () {
                    loadingService.hideLoading();
                });
        });

        return vm;
    };

    angular.module("oisys-app").controller("viewCustomerController", ["customerService", "loadingService", viewCustomerController]);
})();