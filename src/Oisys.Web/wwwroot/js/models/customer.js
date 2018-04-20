(function (module) {
    module.factory("Customer", [function () {
        function Customer(id, name, email, contactNumber, contactPerson) {
            this.id = id || 0;
            this.name = name || "";
            this.email = email || "";
            this.contactNumber = contactNumber || "";
            this.contactPerson = contactPerson || "";
        };

        return Customer;
    }]);
})(angular.module("oisys-app"));