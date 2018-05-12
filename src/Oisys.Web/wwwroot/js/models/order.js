(function (module) {
    module.factory("Order", [function () {
        function Order(id, code, customerId, date, dueDate, discountPercent, details) {
            this.id = id || 0;
            this.code = code || 0;
            this.customerId = customerId || 0;
            this.date = date || new Date();
            this.dueDate = dueDate || new Date();
            this.discountPercent = discountPercent || 0;
            this.details = details || [];

            this.grossAmount = 0;
            this.totalAmount = 0;
            this.discountAmount = 0;

            this.selectedCustomer = undefined;
        };

        Order.prototype = {
            update: function () {
                // Get customerId
                var priceListId = 1;

                if (this.selectedCustomer) {
                    this.customerId = this.selectedCustomer.id;
                    this.discountPercent = this.selectedCustomer.discount;
                    priceListId = this.selectedCustomer.priceListId;
                }
                
                var total = 0;
                if (this.details) {
                    this.details.forEach(function (elem) {
                        if (!elem.isDeleted) {
                            elem.setupDetail(priceListId);

                            if (elem.totalPrice) {
                                total += elem.totalPrice;
                            }
                        }
                    });
                }

                this.grossAmount = total;
                this.discountAmount = total * this.discountPercent / 100;
                this.totalAmount = total - this.discountAmount;
            }
        };

        return Order;
    }]);
})(angular.module("oisys-app"));