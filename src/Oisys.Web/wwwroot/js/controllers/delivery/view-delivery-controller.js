(function (module) {
    var viewDeliveryController = function (deliveryService, customerService, loadingService, $q) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Code",
            sortDirection: "asc",
            searchTerm: "",
            customerId: 0,
            pageIndex: vm.currentPage,
            pageSize: 50
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Code", value: "Code" },
            { text: "Customer", value: "Customer.Name" },
            { text: "Date", value: "Date" },
            { text: "Driver", value: "Driver" },
            { text: "", value: "" }
        ];

        vm.fetchDeliveries = function () {
            loadingService.showLoading();

            deliveryService.fetchDeliveries(vm.filters)
                .then(processDeliveries, onFetchError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.dateFrom = "";
            vm.filters.dateTo = "";
            vm.filters.customerId = 0;
            vm.filters.itemId = 0;

            vm.focus = true;
        };

        // Paging
        vm.changePage = function () {
            vm.fetchDeliveries();
        };

        vm.customerList = [];
        var processFilterList = function (response, copyTo, prop, defaultText) {
            angular.copy(response.data, copyTo);

            var defaultItem = { id: 0 };
            defaultItem[prop] = defaultText;

            copyTo.splice(0, 0, defaultItem);
        };

        var processDeliveries = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var onFetchError = function (error) {
            toastr.error("There was an error processing your request.", "Error");
            console.log(error);
        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        var loadAll = function () {
            loadingService.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                delivery: deliveryService.fetchDeliveries(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processFilterList(responses.customer, vm.customerList, "name", "Filter by customer..");
                    processDeliveries(responses.delivery);
                }, onFetchError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewDeliveryController", ["deliveryService", "customerService", "loadingService", "$q", viewDeliveryController]);

})(angular.module("oisys-app"));