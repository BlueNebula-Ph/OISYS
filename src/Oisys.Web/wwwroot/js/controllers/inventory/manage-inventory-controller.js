(function (module) {

    var manageInventoryController = function () {
        var vm = this;

        vm.sidebarItems = [
            { text: "Manage Inventory", isHeader: true, isItem: false, icon: "fa-database" },
            { text: "Add New Item", isHeader: false, isItem: true, icon: "fa-plus", link: ".add({id:0})" },
            { text: "Search Inventory", isHeader: false, isItem: true, icon: "fa-search", link: ".list" },
            { text: "Manufacturing", isHeader: true, isItem: false, icon: "fa-cog" },
            { text: "Manufacture Item", isHeader: false, isItem: true, icon: "fa-folder-o", link: ".manufacture" },
            { text: "Adjustments", isHeader: true, isItem: false, icon: "fa-exchange" },
            { text: "Adjust Item Qty", isHeader: false, isItem: true, icon: "fa-compress", link: ".adjustment" }
        ];

        return vm;
    };

    module.controller("manageInventoryController", [manageInventoryController]);

})(angular.module("oisys-app"));