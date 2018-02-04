(function (module) {
    var viewCategoryController = function ($q, categoryService, utils) {
        var vm = this;

        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: "",
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: []
        };

        // Headers
        vm.headers = [
            { text: "Name", value: "Name" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchCategories = function () {
            utils.showLoading();

            categoryService.fetchCategories(vm.filters)
                .then(processCategoryList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        // Paging
        vm.changePage = function () {
            vm.fetchCategories();
        };

        vm.delete = function (id) {
            if (!utils.showConfirmMessage("Are you sure you want to delete this category?")) { return; }

            categoryService.deleteCategory(id)
				.then((response) => { vm.fetchCategories(); }, utils.onError)
				.finally(utils.hideLoading);
        };

        var processCategoryList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var processResponses = function (responses) {
            processCategoryList(responses.category);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                category: categoryService.fetchCategories(vm.filters)
            };

            $q.all(requests)
                .then(processResponses, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewCategoryController", ["$q", "categoryService", "utils", viewCategoryController]);

})(angular.module("oisys-app"));