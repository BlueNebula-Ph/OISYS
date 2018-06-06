(function (module) {
    module.factory("DeliveryDetail", ["orderService", "utils", function (orderService, utils) {
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

            this.customerId = 0;
            this.customerOrders = [];
            this.orderDetailId = 0;
            this.max = 1;
        };

        DeliveryDetail.prototype = {
            setupDetail: function () {
                if (this.customerId != 0 && !this.selectedOrderDetail) {
                    utils.showLoading();
                    orderService.getOrderDetailLookup(this.customerId, false)
                        .then((response) => { angular.copy(response.data, this.customerOrders); }, utils.onError)
                        .finally(utils.hideLoading);
                }

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