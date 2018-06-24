(function (module) {
    var loginController = function (authService, loginRedirect, currentUser) {
        var vm = this;
        vm.defaultFocus = true;

        vm.username = "";
        vm.password = "";
        vm.userProfile = currentUser;
        vm.signingIn = false;

        vm.login = function (form) {
            vm.signingIn = true;

            if (form.$valid) {
                authService.login(vm.username, vm.password)
                    .then(function (response) {
                        loginRedirect.redirectPostLogin();
                    }, function (errorResponse) {
                        var errorMesssage = errorResponse ? errorResponse.data.error : "Unable to login.";
                        toastr.error(errorResponse.data.error, "Something went wrong.");
                    }).finally(function () {
                        vm.password = vm.username = "";
                        form.$setUntouched();
                        form.$setPristine();

                        vm.defaultFocus = true;
                        vm.signingIn = false;
                    });
            }
        };

        return vm;
    };

    module.controller("loginController", ["authService", "loginRedirect", "currentUser", loginController]);

})(angular.module("oisys-app"));