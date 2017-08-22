(function (module) {

    var viewItemController = function (inventoryService, loadingService) {

        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: ""
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Code", value: "Code" },
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

            vm.filters.pageIndex = vm.currentPage;

            inventoryService.fetchItems(vm.filters)
                .then(function (response) {
                    angular.copy(response.data, vm.summaryResult);
                }, function (error) {
                    console.log(error);
                })
                .finally(function () {
                    loadingService.hideLoading();
                });
        };

        $(function () {
            vm.fetchItems();
        });

        return vm;
    };

    module.controller("viewItemController", ["inventoryService", "loadingService", viewItemController]);

})(angular.module("oisys-app"));