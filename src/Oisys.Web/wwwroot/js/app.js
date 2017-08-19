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
                .state("system.add-city", {
                    url: "/add/city/{id}",
                    templateUrl: "/views/manage/add-cities.html",
                    controller: "addCityController",
                    controllerAs: "ctrl"
                })
                .state("system.list-cities", {
                    url: "/list/cities",
                    templateUrl: "/views/manage/cities-list.html",
                    controller: "viewCityController",
                    controllerAs: "ctrl"
                })
                .state("system.add-category", {
                    url: "/add/category/{id}",
                    templateUrl: "/views/manage/add-category.html",
                    controller: "addCategoryController",
                    controllerAs: "ctrl"
                })
                .state("system.list-categories", {
                    url: "/list/categories",
                    templateUrl: "/views/manage/category-list.html",
                    controller: "viewCategoryController",
                    controllerAs: "ctrl"
                })
                .state("system.add-user", {
                    url: "/add/user/{id}",
                    template: "<h3>Add New User</h3>"
                })
                .state("system.list-users", {
                    url: "/list/users",
                    template: "<h3>Search Users</h3>"
                })

                .state("customers", {
                    url: "/customers",
                    templateUrl: "/views/common/index.html",
                    controller: "manageCustomerController",
                    controllerAs: "ctrl"
                })
                .state("customers.list", {
                    url: "/list",
                    templateUrl: "/views/customer/customers-list.html",
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
                })
                .state("customers.transactions", {
                    url: "/transactions",
                    template: "<h3>Transactions</h3>"
                })

                .state("inventory", {
                    url: "/inventory",
                    templateUrl: "/views/common/index.html",
                    controller: "manageInventoryController",
                    controllerAs: "ctrl"
                }).state("inventory.list", {
                    url: "/list",
                    template: "<h3>List</h3>"
                });
        }])
        .run(["$rootScope", "$state", "$stateParams",
            function ($rootScope, $state, $stateParams) {
                $rootScope.$state = $state;
                $rootScope.$stateParams = $stateParams;
            }]);;
})();