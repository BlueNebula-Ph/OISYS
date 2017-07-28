(function (module) {

    var customerDetailsController = function () {
        var vm = this;

        vm.customerDetails = {};

        // Initialize the details
        $(function () {

        });

        return vm;
    };

    module.controller("customerDetailsController", [customerDetailsController]);

})(angular.module("oisys-app"));