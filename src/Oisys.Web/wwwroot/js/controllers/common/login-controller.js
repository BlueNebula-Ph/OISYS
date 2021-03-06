﻿(function (module) {

    var loginController = function (authService, currentUser, loginRedirect) {
        var vm = this;

        vm.username = "";
        vm.password = "";
        vm.user = currentUser.userProfile;

        vm.login = function (form) {
            if (form.$valid) {
                authService
                    .login(vm.username, vm.password)
                    .then(function () {
                        loginRedirect.redirectPostLogin();
                    })
                    .catch(function (error) {
                        console.log(error);
                        alert("CANNOT LOGIN!");
                    });

                vm.username = vm.password = "";
                form.$setUntouched();
            }
        };

        return vm;
    };

    module.controller("loginController", ["authService", "currentUser", "loginRedirect", loginController]);

})(angular.module("oisys-app"));