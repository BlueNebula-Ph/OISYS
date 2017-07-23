(function () {
    var loadingService = function () {
        var self = this;
        self.isShown = false;

        self.showLoading = function () {
            toggleLoading(true);
        };

        self.hideLoading = function () {
            toggleLoading(false);
        };

        var toggleLoading = function (visible) {
            self.isShown = visible ? true : false;
        };

        return self;
    };

    angular.module('oisys-app').service('loadingService', [loadingService]);
})();