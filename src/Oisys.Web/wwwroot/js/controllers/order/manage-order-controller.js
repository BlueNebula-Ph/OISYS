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
            { text: "Credit Memo", isHeader: true, isItem: false, icon: "fa-undo" },
            { text: "New Credit Memo", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-creditmemo({ id: 0 })" },
            { text: "View Credit Memo", isHeader: false, isItem: true, icon: "fa-search", link: ".view-creditmemo" },
            { text: "Cash Vouchers", isHeader: true, isItem: false, icon: "fa-file-o" },
            { text: "Add New Voucher", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-voucher({ id: 0 })" },
            { text: "View Vouchers", isHeader: false, isItem: true, icon: "fa-search", link: ".view-vouchers" }
        ];

        $(function () {
            $state.go(".list");
        });

        return vm;
    };

    module.controller("manageOrderController", ["$state", manageOrderController]);

})(angular.module("oisys-app"));