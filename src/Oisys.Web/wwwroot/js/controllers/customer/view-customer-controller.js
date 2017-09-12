(function (module) {

    var viewCustomerController = function (customerService, referenceService, loadingService) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: "",
            provinceId: 0,
            cityId: 0
        };
        vm.summaryResult = {
            items: []
        };
        vm.headers = [
            { text: "Code", value: "Code" },
            { text: "Customer Name", value: "Name" },
            { text: "Email", value: "Email" },
            { text: "City", value: "City.Code" },
            { text: "Province", value: "Province.Code" },
            { text: "Contact Person", value: "ContactPerson" },
            { text: "Contact #", value: "ContactNumber" },
            { text: "", value: "" }
        ];

        vm.fetchCustomers = function () {
            loadingService.showLoading();

            vm.filters.pageIndex = vm.currentPage;

            customerService.fetchCustomers(vm.filters)
                .then(function (response) {
                    angular.copy(response.data, vm.summaryResult);
                }, function (error) {
                    console.log(error);
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.provinceId = 0;
            vm.filters.cityId = 0;

            vm.focus = true;
        };

        var fetchReferences = function (referenceTypeId, copyTo, defaultText) {
            loadingService.showLoading();

            referenceService.getReferenceLookup(referenceTypeId)
                .then(function (response) {
                    angular.copy(response.data, copyTo);
                    copyTo.splice(0, 0, { id: 0, code: defaultText });
                }, function (error) {
                    console.log(error);
                }).finally(function () {
                    loadingService.hideLoading();
                });
        };

        vm.cityList = [];
        var loadCities = function () {
            fetchReferences(2, vm.cityList, "Filter by city..");
        };

        vm.provinceList = [];
        var loadProvinces = function () {
            fetchReferences(3, vm.provinceList, "Filter by province..");
        };

        $(function () {
            loadCities();
            loadProvinces();

            vm.fetchCustomers();
        });

        return vm;
    };

    module.controller("viewCustomerController", ["customerService", "referenceService", "loadingService", viewCustomerController]);

})(angular.module("oisys-app"));