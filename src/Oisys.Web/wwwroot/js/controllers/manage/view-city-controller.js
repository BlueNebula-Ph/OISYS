(function (module) {

    var viewCityController = function (referenceService, loadingService) {
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
        vm.headers = [
            { text: "City", value: "Code" },
            { text: "Province", value: "ParentReference.Code" },
            { text: "", value: "" }
        ];

        vm.fetchCities = function () {
            loadingService.showLoading();

            vm.filters.pageIndex = vm.currentPage;

            referenceService.fetchReferences(2, vm.filters)
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
            vm.fetchCities();
        });

        return vm;
    };

    module.controller("viewCityController", ["referenceService", "loadingService", viewCityController]);

})(angular.module("oisys-app"));