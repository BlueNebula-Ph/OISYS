(function (module) {
    var viewCashVoucherController = function ($q, cashVoucherService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "VoucherNumber",
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
            { text: "Voucher Number", value: "VoucherNumber" },
            { text: "Date", value: "Date", class: "text-center" },
            { text: "Pay To", value: "PayTo" },
            { text: "Category", value: "Category" },
            { text: "Amount", value: "Amount", class: "text-right" },
            { text: "Released By", value: "ReleasedBy" },
            { text: "", value: "" }
        ];

        vm.fetchCashVouchers = function () {
            utils.showLoading();

            cashVoucherService.fetchCashVouchers(vm.filters)
                .then(processCashVouchers, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.dateFrom = "";
            vm.filters.dateTo = "";

            vm.focus = true;
        };

        vm.delete = function (id) {
            if (!confirm("Are you sure you want to delete this cash voucher?")) {
                return;
            }

            cashVoucherService.deleteCashVoucher(id)
                .then((response) => { vm.fetchCashVouchers(); }, utils.onError);
        };

        var processCashVouchers = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            vm.fetchCashVouchers();
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewCashVoucherController", ["$q", "cashVoucherService", "utils", viewCashVoucherController]);

})(angular.module("oisys-app"));