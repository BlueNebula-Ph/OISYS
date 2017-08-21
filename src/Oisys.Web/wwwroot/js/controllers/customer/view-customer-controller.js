(function (module) {

    var viewCustomerController = function (customerService, loadingService) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: ""
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Code", value: "Code" },
            { text: "Customer Name", value: "Name" },
            { text: "Email", value: "Email" },
            { text: "City", value: "City.Code" },
            { text: "Province", value: "Province.Code" },
            { text: "Contact Person", value: "ContactPerson" },
            { text: "Contact #", value: "ContactNumber" },
            { text: "", value: "" }
        ];

        vm.fetchCustomers = function () {
            loadingService.showLoading();

            vm.filters.pageIndex = vm.currentPage;

            customerService.fetchCustomers(vm.filters)
                .then(function (response) {
                    angular.copy(response.data, vm.summaryResult);
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