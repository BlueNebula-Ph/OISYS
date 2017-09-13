(function (module) {

    var viewOrderController = function (orderService, customerService, inventoryService, referenceService, loadingService) {

        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Code",
            sortDirection: "asc",
            searchTerm: "",
            customerId: 0,
            provinceId: 0,
            itemId: 0,
            dateFrom: "",
            dateTo: ""
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Code", value: "Code" },
            { text: "Customer", value: "Customer.Name" },
            { text: "Date", value: "Date" },
            { text: "DueDate", value: "DueDate" },
            { text: "Total Amount", value: "Amount", class: "text-right" },
            { text: "", value: "" }
        ];

        vm.fetchOrders = function () {
            loadingService.showLoading();

            vm.filters.pageIndex = vm.currentPage;

            orderService.fetchOrders(vm.filters)
                .then(function (response) {
                    angular.copy(response.data, vm.summaryResult);
                }, function (error) {
                    console.log(error);
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.dateFrom = "";
            vm.filters.dateTo = "";
            vm.filters.customerId = 0;
            vm.filters.itemId = 0;

            vm.focus = true;
        };

        vm.customerList = [];
        var loadCustomers = function () {
            loadingService.showLoading();

            customerService.getCustomerLookup()
                .then(function (response) {
                    angular.copy(response.data, vm.customerList);
                    vm.customerList.splice(0, 0, { id: 0, name: "Filter by customer.." });
                }, function (error) {
                    console.log(error);
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.itemList = [];
        var loadItems = function () {
            loadingService.showLoading();

            inventoryService.getItemLookup()
                .then(function (response) {
                    angular.copy(response.data, vm.itemList);
                    vm.itemList.splice(0, 0, { id: 0, codeName: "Filter by item.." });
                }, function (error) {
                    console.log(error);
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.provinceList = [];
        var loadProvinces = function () {
            loadingService.showLoading();

            referenceService.getReferenceLookup(3)
                .then(function (response) {
                    angular.copy(response.data, vm.provinceList);
                    vm.provinceList.splice(0, 0, { id: 0, code: "Filter by province.." });
                }, function (error) {
                    console.log(error);
                }).finally(function () {
                    loadingService.hideLoading();
                });

        };

        $(function () {
            loadCustomers();
            loadItems();
            loadProvinces();

            vm.fetchOrders();
        });

        return vm;
    };

    module.controller("viewOrderController", ["orderService", "customerService", "inventoryService", "referenceService", "loadingService", viewOrderController]);

})(angular.module("oisys-app"));