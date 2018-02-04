(function (module) {
    var viewItemController = function (inventoryService, referenceService, loadingService, $q) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: "",
            categoryId: 0,
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Name", value: "Name" },
            { text: "Category", value: "Category.Code" },
            { text: "Current Qty", value: "CurrentQuantity", class: "text-right" },
            { text: "Actual Qty", value: "ActualQuantity", class: "text-right" },
            { text: "Main Price", value: "MainPrice", class: "text-right" },
            { text: "N.E. Price", value: "NEPrice", class: "text-right" },
            { text: "Walk-in Price", value: "WalkInPrice", class: "text-right" },
            { text: "", value: "" }
        ];

        vm.fetchItems = function () {
            loadingService.showLoading();

            inventoryService.fetchItems(vm.filters)
                .then(processItemList, onFetchError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.categoryId = 0;

            vm.focus = true;
        };

        vm.categoryList = [];
        var processCategoryFilter = function (response) {
            angular.copy(response.data, vm.categoryList);
            vm.categoryList.splice(0, 0, { id: 0, code: "Filter by category.." });
        };

        var processItemList = function (response) {
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
                category: referenceService.getReferenceLookup(1),
                item: inventoryService.fetchItems(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processCategoryFilter(responses.category);
                    processItemList(responses.item);
                }, onFetchError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewItemController", ["inventoryService", "referenceService", "loadingService", "$q", viewItemController]);

})(angular.module("oisys-app"));