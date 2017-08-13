(function (module) {

    var manageSystemController = function ($state) {
        var vm = this;

        vm.sidebarItems = [
            { text: "Cities / Provinces", isHeader: true, isItem: false, icon: "fa-map-pin" },
            { text: "Add New City", isHeader: false, isItem: true, icon: "fa-plus", link: "" },
            { text: "Search Cities", isHeader: false, isItem: true, icon: "fa-search", link: "" },
            { text: "Categories", isHeader: true, isItem: false, icon: "fa-bookmark" },
            { text: "Add New Category", isHeader: false, isItem: true, icon: "fa-plus", link: "" },
            { text: "Search Categories", isHeader: false, isItem: true, icon: "fa-search", link: "" },
            { text: "Users", isHeader: true, isItem: false, icon: "fa-user" },
            { text: "Add New User", isHeader: false, isItem: true, icon: "fa-plus", link: "" },
            { text: "Search Users", isHeader: false, isItem: true, icon: "fa-search", link: "" }
        ];

        return vm;
    };

    module.controller("manageSystemController", ["$state", manageSystemController]);

})(angular.module("oisys-app"));