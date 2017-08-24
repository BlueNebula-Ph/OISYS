(function (module) {

    var viewOrderController = function (loadingService) {

        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Code",
            sortDirection: "asc",
            searchTerm: ""
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Code", value: "Code" },
            { text: "Customer", value: "Customer.Name" },
            { text: "Date", value: "Date" },
            { text: "Total Amount", value: "Amount" },
            { text: "", value: "" }
        ];

        vm.fetchOrders = function () {
            // loadingService.showLoading();

            vm.filters.pageIndex = vm.currentPage;

            //referenceService.fetchReferences(2, vm.filters)
            //    .then(function (response) {
            //        angular.copy(response.data, vm.summaryResult);
            //    }, function (error) {
            //        toastr.error("An error has occurred.");
            //    })
            //    .finally(function () {
            //        loadingService.hideLoading();
            //    });
        };

        $(function () {
            vm.fetchOrders();
        });

        return vm;
    };

    module.controller("viewOrderController", ["loadingService", viewOrderController]);

})(angular.module("oisys-app"));