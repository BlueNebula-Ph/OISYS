(function (module) {
    var viewCustomerController = function (customerService, referenceService, loadingService, $q) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: "",
            provinceId: 0,
            cityId: 0,
            pageIndex: vm.currentPage
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

            customerService.fetchCustomers(vm.filters)
                .then(processCustomerList, onFetchError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";
            vm.filters.provinceId = 0;
            vm.filters.cityId = 0;

            vm.focus = true;
        };

        vm.cityList = [];
        vm.provinceList = [];
        var processFilters = function (response, copyTo, defaultText) {
            angular.copy(response.data, copyTo);
            copyTo.splice(0, 0, { id: 0, code: defaultText });
        };

        var processCustomerList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var onFetchError = function (error) {
            toastr.error("There was an error processing your requests.", "error");
            console.log(error);
        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        var loadAll = function () {
            loadingService.showLoading();

            var requests = {
                city: referenceService.getReferenceLookup(2),
                province: referenceService.getReferenceLookup(3),
                customer: customerService.fetchCustomers(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processFilters(responses.city, vm.cityList, "Filter by city..");
                    processFilters(responses.province, vm.provinceList, "Filter by province..");
                    processCustomerList(responses.customer);
                }, onFetchError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewCustomerController", ["customerService", "referenceService", "loadingService", "$q", viewCustomerController]);

})(angular.module("oisys-app"));