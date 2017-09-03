(function (module) {

    var manageDeliveryController = function ($state) {
        var vm = this;

        vm.sidebarItems = [
            { text: "Manage Deliveries", isHeader: true, isItem: false, icon: "fa-truck" },
            { text: "Add New Delivery", isHeader: false, isItem: true, icon: "fa-plus", link: ".add({id:0})" },
            { text: "Search Deliveries", isHeader: false, isItem: true, icon: "fa-search", link: ".list" }
        ];

        $(function () {
            $state.go(".list");
        });

        return vm;
    };

    module.controller("manageDeliveryController", ["$state", manageDeliveryController]);

})(angular.module("oisys-app"));