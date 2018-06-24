(function (module) {
    module.factory("Delivery", ["DeliveryDetail", function (DeliveryDetail) {
        function Delivery(id, code, date, details) {
            this.id = id || 0;
            this.code = code || 0;
            this.date = date || new Date();
            this.details = details || [];

            this.customerOrders = [];
            this.summary = [];
            this.totals = [];
        };

        Delivery.prototype = {
            clearDetails: function () {
                this.details = [];
            },
            update: function () {
                this.clearDetails();

                if (this.customerOrders) {
                    var details = this.details;
                    this.customerOrders.forEach(function (elem) {
                        if (elem.selectedCustomer.orderDetails) {
                            elem.selectedCustomer.orderDetails.forEach(function (detail) {
                                if (detail.deliverQuantity != 0) {
                                    var deliveryDetail = new DeliveryDetail(detail.deliverQuantity, detail);
                                    deliveryDetail.setupDetail();
                                    details.push(deliveryDetail);
                                }
                            });
                        }
                    });
                }

                console.log(this.details);

                this.summary = [];
                var itemsByName = this.groupBy(this.details, "itemCodeName");
                for (var key in itemsByName) {
                    if (key != "undefined") {
                        var totalQuantity = 0
                        itemsByName[key].forEach(function (elem) {
                            totalQuantity += elem.quantity;
                        });

                        this.summary.push({ itemCodeName: key, totalQuantity: totalQuantity });
                    }
                }

                this.totals = [];
                var itemsByCategory = this.groupBy(this.details, "category");
                for (var key in itemsByCategory) {
                    if (key != "undefined") {
                        var totalQuantity = 0
                        itemsByCategory[key].forEach(function (elem) {
                            totalQuantity += elem.quantity;
                        });

                        this.totals.push({ description: "Total " + key + ": " + totalQuantity });
                    }
                }
                this.totals.push({ description: "Total Items: " + this.summary.length })
            },
            groupBy: function (xs, key) {
                return xs.reduce(function (rv, x) {
                    (rv[x[key]] = rv[x[key]] || []).push(x);
                    return rv;
                }, {});
            }
        };

        return Delivery;
    }]);
})(angular.module("oisys-app"));