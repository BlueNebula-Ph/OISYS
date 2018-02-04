(function (module) {
    var addUserController = function ($stateParams, userService, utils) {
        var vm = this;
        var defaultUser = {
            canView: true
        };

        // Data
        vm.user = {};

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;
        vm.accessRights = [
            { text: "Administrator", value: "admin", selected: false },
            { text: "Can View", value: "canView", selected: true },
            { text: "Can Write", value: "canWrite", selected: false },
            { text: "Can Delete", value: "canDelete", selected: false }];

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            var accessRights = [];
            vm.accessRights.forEach((val, idx) => {
                if (val.selected) {
                    accessRights.push(val.value);
                }
                vm.user[val.value] = val.selected;
            });
            vm.user.accessRights = accessRights.join(',');

            userService
                .saveUser($stateParams.id, vm.user)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            resetForm();
        };

        // Private methods
        var resetForm = function () {
            angular.copy(defaultUser, vm.user);
            vm.defaultFocus = true;

            vm.accessRights.forEach((val, idx) => {
                val.selected = defaultUser[val.value];
            });

            if (vm.addUserForm) {
                vm.addUserForm.$setPristine();
            }
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("User saved successfully.");

            // If edit, set defaultItem to newly saved item
            if ($stateParams.id != 0) {
                angular.copy(vm.user, defaultUser);
            }

            resetForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        var processUser = function (response) {
            angular.copy(response.data, defaultUser);
            resetForm();
        };

        var loadUser = function () {
            if ($stateParams.id != 0) {
                utils.showLoading();

                userService.getUserById($stateParams.id)
                    .then(processUser, utils.onError)
                    .finally(utils.hideLoading);
            }
        };

        // Initialize
        $(function () {
            loadUser();
        });

        return vm;
    };

    module.controller("addUserController", ["$stateParams", "userService", "utils", addUserController]);

})(angular.module("oisys-app"));