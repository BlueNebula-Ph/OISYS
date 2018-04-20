(function (module) {
    module.factory("OrderDetail", [function () {
        function OrderDetail(id, quantity, itemId, selectedItem, showEdit, isDeleted, focus) {
            this.id = id || 0;
            this.quantity = quantity || 1;
            this.selectedItem = selectedItem || {};
            this.showEdit = showEdit || true;
            this.isDeleted = isDeleted || false;
            this.focus = focus || true;
        };
        OrderDetail.prototype = {
            get unit() {
                if (this.selectedItem) {
                    return this.selectedItem.unit;
                }
            },
            get itemId() {
                if (this.selectedItem) {
                    return this.selectedItem.id;
                }
            },
            get unitPrice() {
                if (this.selectedItem) {
                    return this.selectedItem.mainPrice;
                }
            },
            get totalPrice() {
                return this.quantity * this.unitPrice;
            },
        };

        return OrderDetail;
    }]);
})(angular.module("oisys-app"));