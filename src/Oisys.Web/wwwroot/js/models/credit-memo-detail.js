(function (module) {
    module.factory("CreditMemoDetail", [function () {
        function CreditMemoDetail(id, quantity, isDeleted, focus) {
            this.id = id || 0;
            this.quantity = quantity || 1;

            this.isDeleted = isDeleted || false;
            this.focus = focus || true;

            this.shouldAddBackToInventory = "false";

            this.unit = "";
            this.itemId = 0;
            this.price = 0;
            this.totalPrice = 0;

            this.selectedOrder = undefined;
            this.selectedOrderDetail = undefined;
        };

        CreditMemoDetail.prototype = {
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

        return CreditMemoDetail;
    }]);
})(angular.module("oisys-app"));