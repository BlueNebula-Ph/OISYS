(function () {
    'use strict';
    angular.module("oisys-app", ["ngRoute"])
        .config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
            $routeProvider
                .when("/", {
                    templateUrl: "/views/home/index.html"
                }).when("/list/customers", { // Customers route
                    templateUrl: "/views/customer/index.html",
                    controller: "",
                    controllerAs: ""
                }).when("/list/orders", { // Orders route
                    templateUrl: "/views/order/index.html",
                    controller: "",
                    controllerAs: ""
                }).when("/list/inventory", { // Inventory route
                    templateUrl: "/views/inventory/index.html",
                    controller: "",
                    controllerAs: ""
                }).when("/list/deliveries", { // Deliveries route
                    templateUrl: "/views/delivery/index.html",
                    controller: "",
                    controllerAs: ""
                }).when("/list/users", { // Users route
                    templateUrl: "/views/user/index.html",
                    controller: "",
                    controllerAs: ""
                }).otherwise({ // Otherwise, route to home
                    redirectTo: "/"
                });

            $locationProvider.html5Mode({
                enabled: false,
                requireBase: false
            });
        }]);
})();