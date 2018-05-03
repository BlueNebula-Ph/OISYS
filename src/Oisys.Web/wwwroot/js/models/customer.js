(function (module) {
    module.factory("Customer", [function () {
        function Customer(id, name, email, contactNumber, contactPerson, address, cityId, provinceId, discount, terms, priceListId) {
            this.id = id || 0;
            this.name = name || "";
            this.email = email || "";
            this.contactNumber = contactNumber || "";
            this.contactPerson = contactPerson || "";
            this.address = address || "";
            
            this.discount = discount || 0;
            this.terms = terms || "";
            this.priceListId = priceListId || "1";

            this.provinceId = provinceId || 0;
            this.cityId = cityId || 0;
        };

        return Customer;
    }]);
})(angular.module("oisys-app"));