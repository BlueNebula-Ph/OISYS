(function (module) {
    var viewProvincesController = function ($q, provinceService, utils) {
        var vm = this;

        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: "",
            pageIndex: vm.currentPage,
            pageSize: 50
        };
        vm.summaryResult = {
            items: []
        };

        // Headers
        vm.headers = [
            { text: "Province", value: "Name" },
            { text: "Cities", value: "" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchProvinces = function () {
            utils.showLoading();

            provinceService.searchProvinces(vm.filters)
                .then(processProvinceList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        // Paging
        vm.changePage = function () {
            vm.fetchProvinces();
        };

        vm.delete = function (id) {
            if (!utils.showConfirmMessage("Are you sure you want to delete this province?")) { return; }

            provinceService.deleteProvince(id)
                .then((response) => { vm.fetchProvinces(); }, utils.onError);
        };

        var processProvinceList = function (response) {
            response.data.items.map((item) => {
                item.cties = item.cities.map((sc) => sc.name).join(', ');
            });

            angular.copy(response.data, vm.summaryResult);
        };

        var processResponses = function (responses) {
            processProvinceList(responses.province);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                province: provinceService.searchProvinces(vm.filters)
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

    module.controller("viewProvincesController", ["$q", "provinceService", "utils", viewProvincesController]);

})(angular.module("oisys-app"));