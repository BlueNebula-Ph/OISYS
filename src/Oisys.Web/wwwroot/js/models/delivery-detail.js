(function (module) {
    module.factory("DeliveryDetail", [function () {
        function DeliveryDetail(id, quantity, isDeleted, focus) {
            this.id = id || 0;
            this.quantity = quantity || 1;

            this.isDeleted = isDeleted || false;
            this.focus = focus || true;

            this.unit = "";
            this.itemId = 0;
            this.price = 0;
            this.totalPrice = 0;

            this.selectedOrder = undefined;
            this.selectedOrderDetail = undefined;
        };

        DeliveryDetail.prototype = {
            setupDetail: function () {
                if (this.selectedOrderDetail) {
                    this.orderDetailId = this.selectedOrderDetail.id;
                    this.itemId = this.selectedOrderDetail.itemId;

                    this.unit = this.selectedOrderDetail.unit;
                    this.price = this.selectedOrderDetail.price;
                    this.totalPrice = this.quantity * this.price;
                }
            }
        };

        return DeliveryDetail;
    }]);
})(angular.module("oisys-app"));