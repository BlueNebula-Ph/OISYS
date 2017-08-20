(function (module) {

    var viewCityController = function (referenceService, loadingService) {
        var vm = this;

        vm.focus = true;
        vm.sort = "Code";
        vm.sortDirection = "asc";
        vm.search = "";

        vm.provinces = [];

        vm.fetchCities = function () {
            loadingService.showLoading();

            var search = {
                sortBy: vm.sort,
                sortDirection: vm.sortDirection,
                searchTerm: vm.search
            };

            referenceService.fetchReferences(2, search)
                .then(function (response) {
                    var data = response.data;

                    vm.provinces = data.items;

                    console.log(vm.provinces);
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