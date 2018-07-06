(function (module) {
    module.factory("InvoiceDetail", [function () {
        function InvoiceDetail(id, quantity, selectedItem, isDeleted, focus) {
            this.id = id || 0;
            this.quantity = quantity || 1;
            this.selectedItem = selectedItem || undefined;

            this.isDeleted = isDeleted || false;
            this.focus = focus || true;

            this.unit = "";
            this.itemId = 0;
            this.price = 0;
            this.totalPrice = 0;
            this.categoryName = "";
        };

        InvoiceDetail.prototype = {
            setupDetail: function (priceList) {
                
            },
        };

        return InvoiceDetail;
    }]);
})(angular.module("oisys-app"));