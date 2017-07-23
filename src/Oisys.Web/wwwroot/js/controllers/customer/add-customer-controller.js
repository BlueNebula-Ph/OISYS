(function () {
    var addCustomerController = function (customerService, loadingService) {
        var vm = this;

        vm.customer = {
            code: "12011",
            priceList: "Walk-In Price"
        };

        vm.save = function () {
            loadingService.showLoading();

            console.log(vm.customer);

            customerService.saveCustomer(0, vm.customer)
                .then(function (response) {
                    console.log(response);
                }, function (response) {
                    console.log(response.data);
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        return vm;
    };

    angular.module("oisys-app").controller("addCustomerController", ["customerService", "loadingService", addCustomerController]);
})();