(function (module) {

    var viewCategoryController = function (referenceService, loadingService) {
        var vm = this;

        vm.search = "";
        vm.sort = "Code";
        vm.sortDirection = "asc";
        vm.focus = true;

        vm.categories = [];

        vm.fetchCategories = function () {
            loadingService.showLoading();

            var search = {
                sortBy: vm.sort,
                sortDirection: vm.sortDirection,
                searchTerm: vm.search
            };

            referenceService.fetchReferences(1, search)
                .then(function (response) {
                    var data = response.data;
                    vm.categories = data.items;
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