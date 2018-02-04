(function (module) {
    var viewUsersController = function ($q, userService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Firstname",
            sortDirection: "asc",
            searchTerm: "",
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: []
        };

        // Headers
        vm.headers = [
            { text: "Username", value: "Username" },
            { text: "Full Name", value: "Firstname" },
            { text: "Is Admin", value: "", class: "text-center" },
            { text: "Can View", value: "", class: "text-center" },
            { text: "Can Write", value: "", class: "text-center" },
            { text: "Can Delete", value: "", class: "text-center" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchUsers = function () {
            utils.showLoading();

            userService.searchUsers(vm.filters)
                .then(processUserList, utils.onFetchError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        // Paging
        vm.changePage = function () {
            vm.fetchUsers();
        };

        vm.delete = function (id) {
            if (!utils.showConfirmMessage("Are you sure you want to delete this user?")) { return; }

            userService.deleteUser(id)
				.then((response) => { vm.fetchUsers(); }, utils.onError)
				.finally(utils.hideLoading);
        };

        var processUserList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                user: userService.searchUsers(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processUserList(responses.user);
                }, utils.onFetchError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewUsersController", ["$q", "userService", "utils", viewUsersController]);

})(angular.module("oisys-app"));