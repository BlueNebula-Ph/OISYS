(function (module) {
    module.factory("Delivery", [function () {
        function Delivery(id, code, date, details) {
            this.id = id || 0;
            this.code = code || 0;
            this.date = date || new Date();
            this.details = details || [];

            this.summary = [];
            this.totals = [];
        };

        Delivery.prototype = {
            clearDetails: function () {
                this.details = [];
            },
            update: function () {
                if (this.details) {
                    this.details.forEach(function (elem) {
                        if (!elem.isDeleted) {
                            elem.setupDetail();
                        }
                    });
                }

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