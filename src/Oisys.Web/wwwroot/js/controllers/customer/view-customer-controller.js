(function (module) {
    var viewCustomerController = function (customerService, referenceService, loadingService, $q, utils) {
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
            { text: "Customer Name", value: "Name" },
            { text: "Email", value: "Email" },
            { text: "City", value: "City.Code" },
            { text: "Province", value: "Province.Code" },
            { text: "Contact Person", value: "ContactPerson" },
            { text: "Contact #", value: "ContactNumber" },
            { text: "", value: "" }
        ];

        vm.fetchCustomers = function () {
            utils.showLoading();

            customerService.fetchCustomers(vm.filters)
                .then(processCustomerList, utils.onError)
                .finally(utils.hideLoading);
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

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                //city: referenceService.getReferenceLookup(2),
                //province: referenceService.getReferenceLookup(3),
                customer: customerService.fetchCustomers(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    //processFilters(responses.city, vm.cityList, "Filter by city..");
                    //processFilters(responses.province, vm.provinceList, "Filter by province..");
                    processCustomerList(responses.customer);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewCustomerController", ["customerService", "referenceService", "loadingService", "$q", "utils", viewCustomerController]);

})(angular.module("oisys-app"));