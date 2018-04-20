(function (module) {
    module.factory("City", [function () {
        function City(id, provinceId, name, focus, isDeleted) {
            this.id = id || 0;
            this.provinceId = provinceId || 0;
            this.name = name || "";
            this.focus = focus || true;
            this.isDeleted = isDeleted || false;
        };

        return City;
    }]);

})(angular.module("oisys-app"));