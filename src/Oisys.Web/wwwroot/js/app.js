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
                .state("system.add-province", {
                    url: "/add/province/{id}",
                    templateUrl: "/views/manage/add-province.html",
                    controller: "addProvinceController",
                    controllerAs: "ctrl"
                })
                .state("system.list-provinces", {
                    url: "/list/provinces",
                    templateUrl: "/views/manage/provinces-list.html",
                    controller: "viewProvincesController",
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
                    templateUrl: "/views/customer/customer-transactions.html",
                    controller: "customerTransactionsController",
                    controllerAs: "ctrl"
                })

                .state("inventory", {
                    url: "/inventory",
                    templateUrl: "/views/common/index.html",
                    controller: "manageInventoryController",
                    controllerAs: "ctrl"
                })
                .state("inventory.list", {
                    url: "/list",
                    templateUrl: "/views/inventory/items-list.html",
                    controller: "viewItemController",
                    controllerAs: "ctrl"
                })
                .state("inventory.add", {
                    url: "/add/{id}",
                    templateUrl: "/views/inventory/add-item.html",
                    controller: "addItemController",
                    controllerAs: "ctrl"
                })
                .state("inventory.detail", {
                    url: "/detail/{id}",
                    templateUrl: "/views/inventory/item-details.html",
                    controller: "itemDetailsController",
                    controllerAs: "ctrl"
                })
                .state("inventory.adjustment", {
                    url: "/adjustment",
                    templateUrl: "/views/inventory/item-adjustment.html",
                    controller: "itemAdjustmentController",
                    controllerAs: "ctrl"
                })

                .state("orders", {
                    url: "/orders",
                    templateUrl: "/views/common/index.html",
                    controller: "manageOrderController",
                    controllerAs: "ctrl"
                })
                .state("orders.list", {
                    url: "/list",
                    templateUrl: "/views/order/orders-list.html",
                    controller: "viewOrderController",
                    controllerAs: "ctrl"
                })
                .state("orders.add", {
                    url: "/add/{id}",
                    templateUrl: "/views/order/add-order.html",
                    controller: "addOrderController",
                    controllerAs: "ctrl"
                })
                .state("orders.detail", {
                    url: "/detail/{id}",
                    templateUrl: "/views/order/order-details.html",
                    controller: "orderDetailsController",
                    controllerAs: "ctrl"
                })
                .state("orders.add-quotation", {
                    url: "/quotation/add",
                    template: "<p>Quotation</p>"
                })

                .state("deliveries", {
                    url: "/deliveries",
                    templateUrl: "/views/common/index.html",
                    controller: "manageDeliveryController",
                    controllerAs: "ctrl"
                })
                .state("deliveries.list", {
                    url: "/list",
                    templateUrl: "/views/delivery/delivery-list.html",
                    controller: "viewDeliveryController",
                    controllerAs: "ctrl"
                })
                .state("deliveries.add", {
                    url: "/add/{id}",
                    template: "<div>Add Delivery</div>"
                })
                .state("deliveries.detail", {
                    url: "/details/{id}",
                    template: "<div>Delivery Details</div>"
                });
        }])
        .run(["$rootScope", "$state", "$stateParams",
            function ($rootScope, $state, $stateParams) {
                $rootScope.$state = $state;
                $rootScope.$stateParams = $stateParams;
            }]);;
})();