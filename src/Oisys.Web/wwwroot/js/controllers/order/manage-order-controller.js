(function (module) {

    var manageOrderController = function ($state) {
        var vm = this;

        vm.sidebarItems = [
            { text: "Orders", isHeader: true, isItem: false, icon: "fa-files-o" },
            { text: "Add New Order", isHeader: false, isItem: true, icon: "fa-plus", link: ".add({ id: 0 })" },
            { text: "Search Orders", isHeader: false, isItem: true, icon: "fa-search", link: ".list" },
            { text: "Quotations", isHeader: true, isItem: false, icon: "fa-files-o" },
            { text: "Make Quotation", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-quotation({ id: 0 })" },
            { text: "View Quotations", isHeader: false, isItem: true, icon: "fa-search", link: ".view-quotations" },
            { text: "Returns", isHeader: true, isItem: false, icon: "fa-undo" },
            { text: "Return Items", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-return({ id: 0 })" },
            { text: "View Returns", isHeader: false, isItem: true, icon: "fa-search", link: ".view-returns" }
        ];

        $(function () {
            $state.go(".list");
        });

        return vm;
    };

    module.controller("manageOrderController", ["$state", manageOrderController]);

})(angular.module("oisys-app"));