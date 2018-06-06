(function (module) {
    var deliveryDetailsController = function (deliveryService, utils, $stateParams) {
        var vm = this;
        vm.deliveryInfo = {};

        var processDelivery = function (response) {
            var data = response.data;
            angular.copy(data, vm.deliveryInfo);

            vm.groupedDetails = groupBy(vm.deliveryInfo.details, "customerName");

            vm.summary = [];
            var itemsByName = groupBy(vm.deliveryInfo.details, "itemCodeName");
            for (var key in itemsByName) {
                if (key != "undefined") {
                    var totalQuantity = 0
                    itemsByName[key].forEach(function (elem) {
                        totalQuantity += elem.quantity;
                    });

                    vm.summary.push({ itemCodeName: key, totalQuantity: totalQuantity });
                }
            }

            vm.totals = [];
            var itemsByCategory = groupBy(vm.deliveryInfo.details, "categoryName");
            for (var key in itemsByCategory) {
                if (key != "undefined") {
                    var totalQuantity = 0
                    itemsByCategory[key].forEach(function (elem) {
                        totalQuantity += elem.quantity;
                    });

                    vm.totals.push({ description: "Total " + key + ": " + totalQuantity });
                }
            }
            vm.totals.push({ description: "Total Items: " + vm.summary.length });
        };

        var groupBy = function (xs, key) {
            return xs.reduce(function (rv, x) {
                (rv[x[key]] = rv[x[key]] || []).push(x);
                return rv;
            }, {});
        };

        // Initialize the details
        $(function () {
            utils.showLoading();

            deliveryService.getDelivery($stateParams.id)
                .then(processDelivery, utils.onError)
                .finally(utils.hideLoading);
        });

        return vm;
    };

    module.controller("deliveryDetailsController", ["deliveryService", "utils", "$stateParams", deliveryDetailsController]);
})(angular.module("oisys-app"));