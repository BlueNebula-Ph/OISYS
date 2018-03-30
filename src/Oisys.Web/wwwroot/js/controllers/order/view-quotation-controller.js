(function (module) {
    var viewQuotationController = function ($q, quotationService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Code",
            sortDirection: "asc",
            searchTerm: "",
            dateFrom: "",
            dateTo: "",
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Quotation #", value: "QuoteNumber" },
            { text: "Customer", value: "Customer.Name" },
            { text: "Date", value: "Date" },
            { text: "Delivery Fee", value: "DeliveryFee", class: "text-right" },
            { text: "Total Amount", value: "Amount", class: "text-right" },
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

            vm.focus = true;
        };

        var processQuotations = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                quotation: quotationService.fetchQuotations(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processQuotations(responses.quotation);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewQuotationController", ["$q", "quotationService", "utils", viewQuotationController]);

})(angular.module("oisys-app"));