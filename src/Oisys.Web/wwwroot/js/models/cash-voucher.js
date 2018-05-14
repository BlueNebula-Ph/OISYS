(function (module) {
    module.factory("CashVoucher", function () {
        function CashVoucher(id, voucherNumber, date, amount) {
            this.id = id || 0;
            this.voucherNumber = voucherNumber || 0;

            this.date = date || new Date();
            this.amount = amount || 0;
        };

        return CashVoucher;
    });

})(angular.module("oisys-app"));