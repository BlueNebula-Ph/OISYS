﻿(function (module) {
    var viewDeliveryController = function (deliveryService, inventoryService, customerService, utils, $q) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "DeliveryNumber",
            sortDirection: "asc",
            searchTerm: "",
            customerId: 0,
            pageIndex: vm.currentPage,
            pageSize: 50
            itemId: 0,
            dateFrom: "",
            dateTo: ""
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "DR #", value: "DeliveryNumber" },
            { text: "Date", value: "Date" },
            { text: "Plate Number", value: "PlateNumber" },
            { text: "Province", value: "ProvinceName" },
            { text: "City", value: "CityName" },
            { text: "", value: "" }
        ];

        vm.fetchDeliveries = function () {
            utils.showLoading();

            deliveryService.fetchDeliveries(vm.filters)
                .then(processDeliveries, utils.onError)
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
            vm.fetchDeliveries();
        };

        vm.customerList = [];
        vm.itemList = [];
        var processDeliveries = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                customer: customerService.getCustomerLookup(),
                item: inventoryService.getItemLookup(),
                delivery: deliveryService.fetchDeliveries(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.customer, vm.customerList, "name", "Filter by customer..");
                    utils.populateDropdownlist(responses.item, vm.itemList, "codeName", "Filter by item..");
                    processDeliveries(responses.delivery);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewDeliveryController", ["deliveryService", "inventoryService", "customerService", "utils", "$q", viewDeliveryController]);

})(angular.module("oisys-app"));