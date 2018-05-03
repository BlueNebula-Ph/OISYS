(function (module) {
    module.factory("Adjustment", function () {
        function Adjustment(id, adjustmentType, remarks) {
            this.id = id || 0;
            this.adjustmentType = adjustmentType || "-1";
            this.adjustmentQuantity = 0;
            this.remarks = remarks || "";

            this.current = 0;
            this.actual = 0;
        };

        return Adjustment;
    });

})(angular.module("oisys-app"));