(function () {
    'use strict';

    // Set the environment variables
    var env = {};

    if (window) {
        Object.assign(env, window._environment);
    };

    angular.module("oisys-app", ["ui.router"])
        .constant("env", env)
        .config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.otherwise("/home");

            $stateProvider
                .state("home", {
                    url: "/home",
                    templateUrl: "/views/home/index.html"
                })

                .state("system", {
                    url: "/system",
                    templateUrl: "/views/common/index.html",
                    controller: "manageSystemController",
                    controllerAs: "ctrl"
                })

                .state("customers", {
                    url: "/customers",
                    templateUrl: "/views/common/index.html",
                    controller: "manageCustomerController",
                    controllerAs: "ctrl"
                })
                .state("customers.list", {
                    url: "/list",
                    templateUrl: "/views/customer/list.html",
                    controller: "viewCustomerController",
                    controllerAs: "ctrl"
                })
                .state("customers.add", {
                    url: "/add/{id}",
                    templateUrl: "/views/customer/add-customer.html",
                    controller: "addCustomerController",
                    controllerAs: "ctrl"
                })
                .state("customers.detail", {
                    url: "/detail/{id}",
                    templateUrl: "/views/customer/customer-details.html",
                    controller: "customerDetailsController",
                    controllerAs: "ctrl"
                });
        }])
        .run(["$rootScope", "$state", "$stateParams",
            function ($rootScope, $state, $stateParams) {
                $rootScope.$state = $state;
                $rootScope.$stateParams = $stateParams;
            }]);;
})();