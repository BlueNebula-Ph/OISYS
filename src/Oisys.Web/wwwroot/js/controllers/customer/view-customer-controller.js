(function (module) {

    var viewCustomerController = function (customerService, loadingService) {
        var vm = this;

        vm.customers = [];
        vm.sort = "Name";
        vm.sortDirection = "asc";

        vm.fetchCustomers = function () {
            loadingService.showLoading();

            customerService.fetchCustomers(1, vm.sort, vm.sortDirection, "search")
                .then(function (response) {
                    vm.customers = response.data.items;
                }, function (error) {
                    console.log(error);
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        $(function () {
            vm.fetchCustomers();
        });

        return vm;
    };

    module.controller("viewCustomerController", ["customerService", "loadingService", viewCustomerController]);

})(angular.module("oisys-app"));