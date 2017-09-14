(function (module) {

    var orderDetailsController = function (orderService, loadingService, $stateParams) {
        var vm = this;

        vm.orderInfo = {};

        // Initialize the details
        $(function () {
            loadingService.showLoading();

            orderService.getOrder($stateParams.id)
                .then(function (response) {
                    var data = response.data;
                    angular.copy(data, vm.orderInfo);
                }, function (error) {
                    console.log(error);
                })
                .finally(function () {
                    loadingService.hideLoading();
                });
        });

        return vm;
    };

    module.controller("orderDetailsController", ["orderService", "loadingService", "$stateParams", orderDetailsController]);

})(angular.module("oisys-app"));