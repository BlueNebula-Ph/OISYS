(function (module) {
    var customerTransactionsController = function (customerService, utils, $q) {
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
            { text: "Credit", value: "Credit", class: "text-right" }
        ];

        vm.fetchTransactions = function () {
            utils.showLoading();

            customerService.fetchCustomerTransactions(vm.filters)
                .then(processTransactionList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.dateFrom = "";
            vm.filters.dateTo = "";
            vm.filters.customerId = 0;

            vm.focus = true;
        };

        vm.customerList = [];
        var processTransactionList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup()
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "name", "Filter by customer...");
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("customerTransactionsController", ["customerService", "utils", "$q", customerTransactionsController]);

})(angular.module("oisys-app"));