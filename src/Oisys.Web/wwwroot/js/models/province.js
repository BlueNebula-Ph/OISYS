(function (module) {
    module.factory("Province", ["City", function (City) {
        function Province(id, name, cities) {
            this.id = id || 0;
            this.name = name || "";
            this.cities = cities || [];
        };

        return Province;
    }]);
})(angular.module("oisys-app"));