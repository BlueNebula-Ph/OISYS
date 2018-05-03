(function (module) {
    module.factory("Category", [function () {
        function Category(id, name) {
            this.id = id || 0;
            this.name = name || "";
        };

        return Category;
    }]);

})(angular.module("oisys-app"));