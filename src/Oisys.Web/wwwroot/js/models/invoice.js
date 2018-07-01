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
                
            }
        };

        return Invoice;
    }]);
})(angular.module("oisys-app"));