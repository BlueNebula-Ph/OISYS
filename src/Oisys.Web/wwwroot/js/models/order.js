(function (module) {
    module.factory("Order", [function () {
        function Order(id, code, customerId, date, dueDate, discountPercent, details) {
            this.id = id || 0;
            this.code = code || "";
            this.customerId = customerId || 0;
            this.date = date || new Date();
            this.dueDate = dueDate || new Date();
            this.discountPercent = discountPercent || 0;
            this.details = details || [];
        };
        Order.prototype = {
            get discountAmount() {
                return this.totalAmount * this.discountPercent / 100;
            },
            get totalAmount() {
                var total = 0;
                if (this.details) {
                    this.details.forEach(function (elem) {
                        if (elem.totalPrice) {
                            total += elem.totalPrice;
                        }
                    });
                }
                return total;
            },
        };

        return Order;
    }]);
})(angular.module("oisys-app"));