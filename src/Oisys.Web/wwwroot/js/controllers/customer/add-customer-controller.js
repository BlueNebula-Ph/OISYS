(function (module) {

    var addCustomerController = function (customerService, loadingService) {
        var vm = this;

        vm.defaultFocus = true;

        vm.customer = {
            code: "12011",
            priceList: "Walk-In Price"
        };

        vm.save = function () {
            // loadingService.showLoading();

            alert("SAVE!!");

            //customerService.saveCustomer(0, vm.customer)
            //    .then(function (response) {
            //        toastr.success("SUCCESS!");
            //    }, function (response) {
            //        console.log(response.data);
            //    }).finally(function () {
            //        loadingService.hideLoading();
            //    });
        };

        return vm;
    };

    module.controller("addCustomerController", ["customerService", "loadingService", addCustomerController]);

})(angular.module("oisys-app"));