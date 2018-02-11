(function (module) {

    var manageOrderController = function ($state) {
        var vm = this;

        vm.sidebarItems = [
            { text: "Orders", isHeader: true, isItem: false, icon: "fa-files-o" },
            { text: "Add New Order", isHeader: false, isItem: true, icon: "fa-plus", link: ".add({ id: 0 })" },
            { text: "Search Orders", isHeader: false, isItem: true, icon: "fa-search", link: ".list" },
            { text: "Quotations", isHeader: true, isItem: false, icon: "fa-files-o" },
            { text: "Make Quotation", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-quotation" },
            { text: "View Quotations", isHeader: false, isItem: true, icon: "fa-search", link: ".view-quotations" }
        ];

        $(function () {
            $state.go(".list");
        });

        return vm;
    };

    module.controller("manageOrderController", ["$state", manageOrderController]);

})(angular.module("oisys-app"));