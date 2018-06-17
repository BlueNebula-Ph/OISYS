(function (module) {
    var viewQuotationController = function ($q, quotationService, customerService, inventoryService, provinceService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "QuoteNumber",
            sortDirection: "asc",
            searchTerm: "",
            customerId: 0,
            provinceId: 0,
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
            { text: "Quote Number", value: "QuoteNumber" },
            { text: "Customer", value: "Customer.Name" },
            { text: "Address", value: "Customer.Address" },
            { text: "Date", value: "Date", class: "text-center" },
            { text: "Delivery Fee", value: "DeliveryFee", class: "text-right" },
            { text: "Total", value: "TotalAmount", class: "text-right" },
            { text: "", value: "" }
        ];

        vm.fetchQuotations = function () {
            utils.showLoading();

            quotationService.fetchQuotations(vm.filters)
                .then(processQuotations, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.dateFrom = "";
            vm.filters.dateTo = "";
            vm.filters.customerId = 0;
            vm.filters.itemId = 0;
            vm.filters.provinceId = 0;

            vm.focus = true;
        };

        // Paging
        vm.changePage = function () {
            vm.fetchQuotations();
        };

        vm.customerList = [];
        vm.itemList = [];
        vm.provinceList = [];
        var processQuotations = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup(),
                province: provinceService.getProvinceLookup(),
                quotation: quotationService.fetchQuotations(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "name", "Filter by customer..");
                    utils.populateDropdownlist(responses.item, vm.itemList, "codeName", "Filter by item..");
                    utils.populateDropdownlist(responses.province, vm.provinceList, "name", "Filter by province..");
                    processQuotations(responses.quotation);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewQuotationController", ["$q", "quotationService", "customerService", "inventoryService", "provinceService", "utils", viewQuotationController]);

})(angular.module("oisys-app"));