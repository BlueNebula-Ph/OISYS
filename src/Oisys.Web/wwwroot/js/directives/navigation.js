(function (module) {
    var navigation = function () {
        var navController = ["$state", "currentUser", "localStorage", "keys", "PermPermissionStore",
            function ($state, currentUser, localStorage, keys, PermPermissionStore) {
                var vm = this;

                vm.loggedUser = currentUser.userProfile;

                vm.logout = function () {
                    // Clear the user key from the local storage
                    localStorage.remove(keys.userkey);
                    currentUser.setUserProfile("", "");

                    // Clear the user permissions from the permission store
                    PermPermissionStore.clearStore();

                    $state.go("login");
                };

                return vm;
            }];

        return {
            restrict: "E",
            controller: navController,
            controllerAs: "navCtrl",
            templateUrl: "/views/common/navigation.html?" + $.now()
        };
    };

    module.directive("navigation", [navigation]);

})(angular.module("oisys-app"));