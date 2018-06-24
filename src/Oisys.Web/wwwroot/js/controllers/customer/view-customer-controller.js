(function (module) {
    var viewCustomerController = function ($scope, $q, customerService, provinceService, utils) {
        var vm = this;

        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: "",
            provinceId: 0,
            cityId: 0,
            pageIndex: vm.currentPage,
            pageSize: 50
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

        // Public Methods
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

        // Paging
        vm.changePage = function () {
            vm.fetchCustomers();
        };

        vm.cityList = [{ id: 0, name: "Filter by city.." }];
        vm.provinceList = [];

        vm.delete = function (id) {
            if (!confirm("Are you sure you want to delete this customer?")) {
                return;
            }

            customerService.deleteCustomer(id)
                .then((response) => { vm.fetchCustomers() }, utils.onError);
        };

        // Watchers
        $scope.$watch(function () {
            return vm.filters.provinceId;
        }, function (newVal, oldVal) {
            var selectedProvince = vm.provinceList.find(function (elem) { return elem.id == newVal; });
            if (selectedProvince) {
                utils.populateDropdownlist({ data: selectedProvince.cities }, vm.cityList, "name", "Filter by city..");
            }
        }, true);

        // Private Methods
        var processCustomerList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                province: provinceService.getProvinceLookup(),
                customer: customerService.fetchCustomers(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.province, vm.provinceList, "name", "Filter by province..");
                    processCustomerList(responses.customer);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewCustomerController", ["$scope", "$q", "customerService", "provinceService", "utils", viewCustomerController]);

})(angular.module("oisys-app"));