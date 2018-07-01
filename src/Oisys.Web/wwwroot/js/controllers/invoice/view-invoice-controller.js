(function (module) {
    var viewInvoiceController = function (invoiceService, customerService, utils, $q) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "InvoiceNumber",
            sortDirection: "asc",
            searchTerm: "",
            customerId: 0,
            pageIndex: vm.currentPage,
            pageSize: 50,
            dateFrom: "",
            dateTo: ""
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Invoice #", value: "InvoiceNumber" },
            { text: "Date", value: "Date" },
            { text: "Total Amount", value: "TotalAmount", class: "text-right" },
            { text: "", value: "" }
        ];

        vm.fetchInvoices = function () {
            utils.showLoading();

            invoiceService.fetchInvoices(vm.filters)
                .then(processInvoices, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.dateFrom = "";
            vm.filters.dateTo = "";
            vm.filters.customerId = 0;

            vm.focus = true;
        };

        // Paging
        vm.changePage = function () {
            vm.fetchInvoices();
        };

        vm.customerList = [];
        var processInvoices = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                invoice: invoiceService.fetchInvoices(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "name", "Filter by customer..");
                    processInvoices(responses.invoice);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewInvoiceController", ["invoiceService", "customerService", "utils", "$q", viewInvoiceController]);

})(angular.module("oisys-app"));