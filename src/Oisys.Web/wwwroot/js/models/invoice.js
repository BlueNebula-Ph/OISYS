(function (module) {
    module.factory("Invoice", [function () {
        function Invoice(id, invoiceNumber, customerId, date, discountPercent, details) {
            this.id = id || 0;
            this.invoiceNumber = invoiceNumber || 0;
            this.customerId = customerId || 0;
            this.date = date || new Date();
            this.discountPercent = discountPercent || 0;
            this.details = details || [];

            this.grossAmount = 0;
            this.totalAmount = 0;
            this.discountAmount = 0;

            this.selectedCustomer = undefined;
        };

        Invoice.prototype = {
            update: function () {
                if (this.selectedCustomer) {
                    this.customerId = this.selectedCustomer.id;
                }

                var total = 0;
                this.details.forEach(function (elem) {
                    // Set order id for passing to controller
                    elem.id = 0;

                    if (!elem.isDeleted) {
                        if (elem.totalAmount) {
                            total += elem.totalAmount;
                        }
                    }
                });

                this.grossAmount = total;
                this.discountAmount = total * this.discountPercent / 100;
                this.totalAmount = total - this.discountAmount;
            }
        };

        return Invoice;
    }]);
})(angular.module("oisys-app"));