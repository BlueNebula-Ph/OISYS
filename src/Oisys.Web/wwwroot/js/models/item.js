(function (module) {
    module.factory("Item", function () {
        function Item(id, code, name, description, mainPrice, nEPrice, walkInPrice, weight, thickness, unit, categoryId, quantity) {
            this.id = id || 0;
            this.code = code || "";
            this.name = name || "";
            this.description = description || "";
            this.mainPrice = mainPrice || 0;
            this.nePrice = nEPrice || 0;
            this.walkInPrice = walkInPrice || 0;
            this.weight = weight || "";
            this.thickness = thickness || "";
            this.unit = unit || "";
            this.categoryId = categoryId || 0;
            this.quantity = quantity || 0;
        };

        Item.prototype = {
            setQuantity: function () {
                this.quantity = this.actualQuantity || 0;
            }
        };

        return Item;
    });

})(angular.module("oisys-app"));