(function (module) {
    module.factory("Delivery", [function () {
        function Delivery(id, code, customerId, date, details) {
            this.id = id || 0;
            this.code = code || 0;
            this.customerId = customerId || 0;
            this.date = date || new Date();
            this.details = details || [];

            this.totalAmount = 0;
            this.selectedCustomer = undefined;
        };

        Delivery.prototype = {
            clearDetails: function () {
                this.details = [];
            },
            update: function () {
                if (this.selectedCustomer) {
                    this.customerId = this.selectedCustomer.id;
                }

                var total = 0;
                if (this.details) {
                    this.details.forEach(function (elem) {
                        if (!elem.isDeleted) {
                            elem.setupDetail();

                            if (elem.totalPrice) {
                                total += elem.totalPrice;
                            }
                        }
                    });
                }

                this.totalAmount = total;
            }
        };

        return Delivery;
    }]);
})(angular.module("oisys-app"));