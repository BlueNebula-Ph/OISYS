(function (module) {

    var manageCustomerController = function ($state) {
        var vm = this;

        vm.sidebarItems = [
            { text: "Customer Accounts", isHeader: true, isItem: false, icon: "fa-address-card-o" },
            { text: "Add New Customer", isHeader: false, isItem: true, icon: "fa-plus", link: ".add({ id: 0 })" },
            { text: "Search Customers", isHeader: false, isItem: true, icon: "fa-search", link: ".list" },
            { text: "Transactions", isHeader: true, isItem: false, icon: "fa-credit-card" },
            { text: "View Transactions", isHeader: false, isItem: true, icon: "fa-search", link: ".transactions" },
            { text: "Invoices", isHeader: true, isItem: false, icon: "fa-file-o" },
            { text: "Create Invoice", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-invoice({ id: 0 })" },
            { text: "View Invoices", isHeader: false, isItem: true, icon: "fa-search", link: ".invoice" }
        ];

        // Show lists page as default view
        $(function () {
            $state.go(".list");
        });

        return vm;
    };

    module.controller("manageCustomerController", ["$state", manageCustomerController]);

})(angular.module("oisys-app"));