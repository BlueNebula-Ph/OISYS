(function (module) {
    var addUserController = function ($stateParams, userService, utils, User, modelTransformer) {
        var vm = this;
        vm.user = new User();

        // Helper properties
        vm.defaultFocus = true;
        vm.isSaving = false;

        // Public methods
        vm.save = function () {
            vm.isSaving = true;
            vm.user.setAccessRights();

            userService.saveUser($stateParams.id, vm.user)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        // Private methods
        var resetForm = function () {
            vm.addUserForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("User saved successfully.");

            if ($stateParams.id == 0) {
                vm.user = new User();
            }

            resetForm();
        };

        var onSaveComplete = function () {
            vm.isSaving = false;
        };

        var processUser = function (response) {
            vm.user = modelTransformer.transform(response.data, User);
            vm.user.refreshAccessList();
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

    module.controller("addUserController", ["$stateParams", "userService", "utils", "User", "modelTransformer", addUserController]);

})(angular.module("oisys-app"));