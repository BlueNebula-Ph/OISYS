(function (module) {
    var customerTransactionsController = function (customerService, loadingService, $q) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Date",
            sortDirection: "desc",
            customerId: 0,
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: [],
            totalPages: 0
        };
        vm.headers = [
            { text: "Customer", value: "Customer.Name" },
            { text: "Date", value: "Date", class: "text-center" },
            { text: "Description", value: "Description" },
            { text: "Debit", value: "Debit", class: "text-right" },
            { text: "Credit", value: "Credit", class: "text-right" },
            { text: "Running Balance", value: "RunningBalance", class: "text-right" }
        ];

        vm.fetchTransactions = function () {
            loadingService.showLoading();

            console.log(vm.filters);

            customerService.fetchCustomerTransactions(vm.filters)
                .then(processTransactionList, onFetchError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.dateFrom = "";
            vm.filters.dateTo = "";
            vm.filters.customerId = 0;

            console.log(vm.filters);

            vm.focus = true;
        };

        vm.customerList = [];
        var processFilters = function (response, copyTo, defaultText) {
            angular.copy(response.data, copyTo);
            copyTo.splice(0, 0, { id: 0, name: defaultText });
        };

        var processTransactionList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var onFetchError = function (error) {
            toastr.error("There was an error processing your requests.", "error");
            console.log(error);
        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        var loadAll = function () {
            loadingService.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup()
            };

            $q.all(requests)
                .then((responses) => {
                    processFilters(responses.customer, vm.customerList, "Filter by customer..");
                }, onFetchError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("customerTransactionsController", ["customerService", "loadingService", "$q", customerTransactionsController]);

})(angular.module("oisys-app"));