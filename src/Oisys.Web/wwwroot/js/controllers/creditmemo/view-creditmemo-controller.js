(function (module) {
    var viewCreditMemoController = function ($q, creditMemoService, customerService, inventoryService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Code",
            sortDirection: "asc",
            searchTerm: "",
            customerId: 0,
            itemId: 0,
            dateFrom: "",
            dateTo: "",
            pageIndex: vm.currentPage,
            pageSize: 50
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Code", value: "Code" },
            { text: "Customer", value: "Customer.Name" },
            { text: "Date", value: "Date", class: "text-center" },
            { text: "Driver", value: "Driver" },
            { text: "Total Amount", value: "TotalAmount", class: "text-right" },
            { text: "", value: "" }
        ];

        vm.fetchCreditMemos = function () {
            utils.showLoading();

            creditMemoService.fetchCreditMemos(vm.filters)
                .then(processCreditMemos, utils.onError)
                .finally(utils.hideLoading);
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
            vm.fetchCreditMemos();
        };

        vm.customerList = [];
        vm.itemList = [];
        var processCreditMemos = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup(),
                order: creditMemoService.fetchCreditMemos(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "name", "Filter by customer..");
                    utils.populateDropdownlist(responses.item, vm.itemList, "codeName", "Filter by item..");
                    processCreditMemos(responses.order);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewCreditMemoController", ["$q", "creditMemoService", "customerService", "inventoryService", "utils", viewCreditMemoController]);

})(angular.module("oisys-app"));