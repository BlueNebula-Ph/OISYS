(function (module) {
    var viewOrderController = function ($q, orderService, customerService, inventoryService, provinceService, utils) {
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
            dateTo: "",
            pageIndex: vm.currentPage,
            pageSize: 50
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Order #", value: "Code" },
            { text: "Customer", value: "Customer.Name" },
            { text: "Date", value: "Date" },
            { text: "Due Date", value: "DueDate" },
            { text: "Total Amount", value: "Amount", class: "text-right" },
            { text: "", value: "" }
        ];

        vm.fetchOrders = function () {
            utils.showLoading();

            orderService.fetchOrders(vm.filters)
                .then(processOrders, utils.onError)
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
            vm.fetchOrders();
        };

        vm.customerList = [];
        vm.itemList = [];
        vm.provinceList = [];
        var processOrders = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup(),
                province: provinceService.getProvinceLookup(),
                order: orderService.fetchOrders(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "name", "Filter by customer..");
                    utils.populateDropdownlist(responses.item, vm.itemList, "codeName", "Filter by item..");
                    utils.populateDropdownlist(responses.province, vm.provinceList, "name", "Filter by province..");
                    processOrders(responses.order);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewOrderController", ["$q", "orderService", "customerService", "inventoryService", "provinceService", "utils", viewOrderController]);

})(angular.module("oisys-app"));