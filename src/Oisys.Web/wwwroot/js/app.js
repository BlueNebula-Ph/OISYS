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
            //$routeProvider
            //    .when("/", {
            //        templateUrl: "/views/home/index.html"
            //    }).when("/login", {
            //        templateUrl: "/views/common/login.html",
            //        controller: "loginController",
            //        controllerAs: "ctrl"
            //    }).when("/list/customers", { // Customer routes
            //        templateUrl: "/views/customer/index.html",
            //        controller: "viewCustomerController",
            //        controllerAs: "ctrl"
            //    }).when("/add/customer/:id", {
            //        templateUrl: "/views/customer/add-customer.html",
            //        controller: "addCustomerController",
            //        controllerAs: "ctrl"
            //    }).when("/details/customer/:id", {
            //        templateUrl: "/views/customer/customer-details.html",
            //        controller: "customerDetailsController",
            //        controllerAs: "ctrl"
            //    }).when("/list/orders", { // Orders route
            //        templateUrl: "/views/order/index.html",
            //        controller: "",
            //        controllerAs: ""
            //    }).when("/list/inventory", { // Inventory route
            //        templateUrl: "/views/inventory/index.html",
            //        controller: "",
            //        controllerAs: ""
            //    }).when("/list/deliveries", { // Deliveries route
            //        templateUrl: "/views/delivery/index.html",
            //        controller: "",
            //        controllerAs: ""
            //    }).when("/list/users", { // Users route
            //        templateUrl: "/views/user/index.html",
            //        controller: "",
            //        controllerAs: ""
            //    }).when("/references", { // References route
            //        templateUrl: "/views/reference/index.html",
            //        controller: "",
            //        controllerAs: ""
            //    }).otherwise({ // Otherwise, route to home
            //        redirectTo: "/"
            //    });

            //$locationProvider.html5Mode({
            //    enabled: false,
            //    requireBase: false
            //});

            $urlRouterProvider.otherwise("/home");

            $stateProvider
                .state("home", {
                    url: "/home",
                    templateUrl: "/views/home/index.html"
                })

                .state("customers", {
                    url: "/manage/customers",
                    templateUrl: "/views/customer/index.html",
                    controller: "viewCustomerController",
                    controllerAs: "ctrl"
                });
        }]);
})();