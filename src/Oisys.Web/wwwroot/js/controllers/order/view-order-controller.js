(function (module) {
    var viewOrderController = function (orderService, customerService, inventoryService, referenceService, loadingService, $q) {
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
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Code", value: "Code" },
            { text: "Customer", value: "Customer.Name" },
            { text: "Date", value: "Date" },
            { text: "Due Date", value: "DueDate" },
            { text: "Total Amount", value: "Amount", class: "text-right" },
            { text: "", value: "" }
        ];

        vm.fetchOrders = function () {
            loadingService.showLoading();

            orderService.fetchOrders(vm.filters)
                .then(processOrders, onFetchError)
                .finally(hideLoading);
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

        vm.customerList = [];
        vm.itemList = [];
        vm.provinceList = [];
        var processFilterList = function (response, copyTo, prop, defaultText) {
            angular.copy(response.data, copyTo);

            var defaultItem = { id: 0 };
            defaultItem[prop] = defaultText;

            copyTo.splice(0, 0, defaultItem);
        };

        var processOrders = function (response) {
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
                item: inventoryService.getItemLookup(),
                province: referenceService.getReferenceLookup(3),
                order: orderService.fetchOrders(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processFilterList(responses.customer, vm.customerList, "name", "Filter by customer..");
                    processFilterList(responses.item, vm.itemList, "codeName", "Filter by item..");
                    processFilterList(responses.province, vm.provinceList, "code", "Filter by province..");
                    processOrders(responses.order);
                }, onFetchError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewOrderController", ["orderService", "customerService", "inventoryService", "referenceService", "loadingService", "$q", viewOrderController]);

})(angular.module("oisys-app"));