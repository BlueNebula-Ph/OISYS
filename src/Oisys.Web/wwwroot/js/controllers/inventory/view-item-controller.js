(function (module) {
    var viewItemController = function ($q, inventoryService, categoryService, utils) {
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
            { text: "Category", value: "Category.Name" },
            { text: "Current Qty", value: "CurrentQuantity", class: "text-right" },
            { text: "Actual Qty", value: "ActualQuantity", class: "text-right" },
            { text: "Main Price", value: "MainPrice", class: "text-right" },
            { text: "Walk-in Price", value: "WalkInPrice", class: "text-right" },
            { text: "N.E. Price", value: "NEPrice", class: "text-right" },
            { text: "", value: "" }
        ];

        vm.fetchItems = function () {
            utils.showLoading();

            inventoryService.fetchItems(vm.filters)
                .then(processItemList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.categoryId = 0;

            vm.focus = true;
        };

        // Paging
        vm.changePage = function () {
            vm.fetchItems();
        };

        vm.deleteItem = function (id) {
            if (!confirm("Are you sure you want to delete this item?")) {
                return;
            }

            inventoryService.deleteItem(id)
                .then((response) => { vm.fetchItems(); }, utils.onError);
        };

        vm.categoryList = [];
        var processItemList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                category: categoryService.getCategoryLookup(),
                item: inventoryService.fetchItems(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.category, vm.categoryList, "name", "Filter by category..");
                    processItemList(responses.item);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewItemController", ["$q", "inventoryService", "categoryService", "utils", viewItemController]);

})(angular.module("oisys-app"));