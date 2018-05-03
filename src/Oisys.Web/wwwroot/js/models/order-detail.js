(function (module) {
    module.factory("OrderDetail", [function () {
        function OrderDetail(id, quantity, selectedItem, isDeleted, focus) {
            this.id = id || 0;
            this.quantity = quantity || 1;
            this.selectedItem = selectedItem || undefined;

            this.isDeleted = isDeleted || false;
            this.focus = focus || true;

            this.unit = "";
            this.itemId = 0;
            this.price = 0;
            this.totalPrice = 0;
        };

        OrderDetail.prototype = {
            setupDetail: function (priceList) {
                if (this.selectedItem) {
                    this.itemId = this.selectedItem.id;
                    this.unit = this.selectedItem.unit;

                    switch (priceList) {
                        case 1:
                            this.price = this.selectedItem.mainPrice;
                            break;
                        case 2:
                            this.price = this.selectedItem.walkInPrice;
                            break;
                        case 3:
                            this.price = this.selectedItem.nePrice;
                            break;
                        default:
                            this.price = this.selectedItem.mainPrice;
                    }

                    this.totalPrice = this.quantity * this.price;
                }
            },
        };

        return OrderDetail;
    }]);
})(angular.module("oisys-app"));