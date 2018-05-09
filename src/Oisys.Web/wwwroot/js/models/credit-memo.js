(function (module) {
    module.factory("CreditMemo", [function () {
        function CreditMemo(id, code, customerId, date, details) {
            this.id = id || 0;
            this.code = code || 0;
            this.customerId = customerId || 0;
            this.date = date || new Date();
            this.details = details || [];

            this.totalAmount = 0;
        };

        CreditMemo.prototype = {
            clearDetails: function () {
                this.details = [];
            },
            update: function () {
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

        return CreditMemo;
    }]);
})(angular.module("oisys-app"));