(function (module) {

    var viewCategoryController = function (referenceService, loadingService) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Code",
            sortDirection: "asc",
            searchTerm: ""
        };
        vm.summaryResult = {
            items: []
        };

        vm.fetchCategories = function () {
            loadingService.showLoading();

            vm.filters.pageIndex = vm.currentPage;

            referenceService.fetchReferences(1, vm.filters)
                .then(function (response) {
                    angular.copy(response.data, vm.summaryResult);
                }, function (error) {
                    toastr.error("An error has occurred.");
                })
                .finally(function () {
                    loadingService.hideLoading();
                });
        };

        $(function () {
            vm.fetchCategories();
        });

        return vm;
    };

    module.controller("viewCategoryController", ["referenceService", "loadingService", viewCategoryController]);

})(angular.module("oisys-app"));