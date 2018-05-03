(function (module) {
    module.factory("Quotation", function () {
        function Quotation(id, quoteNumber, customerId, date, deliveryFee, details) {
            this.id = id || 0;
            this.quoteNumber = quoteNumber || "";
            this.customerId = customerId || 0;
            this.date = date || new Date();
            this.deliveryFee = deliveryFee || 0;
            this.details = details || [];

            this.selectedCustomer = undefined;
        };

        Quotation.prototype = {
            update: function () {
                var priceListId = 1;

                if (this.selectedCustomer) {
                    this.customerId = this.selectedCustomer.id;
                    this.address = this.selectedCustomer.address;
                    this.contactNumber = this.selectedCustomer.contactNumber;
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

                this.totalAmount = total;
            }
        };

        return Quotation;
    });

})(angular.module("oisys-app"));