﻿(function (module) {

    var manageSystemController = function () {
        var vm = this;

        vm.sidebarItems = [
            { text: "Provinces", isHeader: true, isItem: false, icon: "fa-map-o" },
            { text: "Add Province", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-province({ id: 0 })" },
            { text: "Search Provinces", isHeader: false, isItem: true, icon: "fa-search", link: ".list-provinces" },
            { text: "Categories", isHeader: true, isItem: false, icon: "fa-file-o" },
            { text: "Add New Category", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-category({ id: 0 })" },
            { text: "Search Categories", isHeader: false, isItem: true, icon: "fa-search", link: ".list-categories" },
            { text: "Users", isHeader: true, isItem: false, icon: "fa-user-circle" },
            { text: "Add New User", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-user({ id: 0 })" },
            { text: "Search Users", isHeader: false, isItem: true, icon: "fa-search", link: ".list-users" }
        ];

        return vm;
    };

    module.controller("manageSystemController", [manageSystemController]);

})(angular.module("oisys-app"));