(function (module) {
    module.factory("DeliveryDetail", [function () {
        function DeliveryDetail(quantity, selectedOrderDetail) {
            this.id = 0;
            this.quantity = quantity || 1;

            this.isDeleted = false;
            this.focus = true;

            this.unit = "";
            this.itemId = 0;
            this.price = 0;
            this.totalPrice = 0;

            this.selectedOrderDetail = selectedOrderDetail || undefined;

            this.orderDetailId = 0;
            this.max = 1;
        };

        DeliveryDetail.prototype = {
            setupDetail: function () {
                if (this.selectedOrderDetail) {
                    this.itemCodeName = this.selectedOrderDetail.itemCodeName;
                    this.category = this.selectedOrderDetail.category;
                    this.orderDetailId = this.selectedOrderDetail.id;
                    this.max = this.selectedOrderDetail.quantity - this.selectedOrderDetail.quantityDelivered;
                }
            }
        };

        return DeliveryDetail;
    }]);
})(angular.module("oisys-app"));