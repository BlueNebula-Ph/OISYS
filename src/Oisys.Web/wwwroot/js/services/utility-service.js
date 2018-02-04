(function (module) {
    var utils = function (loadingService) {
        var service = {};

        service.showSuccessMessage = function (message) {
            toastr.success(message, "Success");
        };

        service.showErrorMessage = function (message) {
            toastr.error(message, "Error");
        };

        service.onError = function (errorResponse) {
            var errorMessage = "There was an error processing your requests.";

            if (errorResponse.data.errorMessage) {
                errorMessage = errorResponse.data.errorMessage;
            }

            service.showErrorMessage(errorMessage);
            console.log(errorResponse);
        };

        service.showLoading = function () {
            loadingService.showLoading();
        };

        service.hideLoading = function () {
            loadingService.hideLoading();
        };

        service.populateDropdownlist = function (response, copyTo, prop, defaultText) {
            var data = response.data;
            angular.copy(data, copyTo);

            if (defaultText != "") {
                var defaultItem = { id: 0 };
                defaultItem[prop] = defaultText;

                copyTo.splice(0, 0, defaultItem);
            }
        };

        service.showConfirmMessage = function (message) {
            return confirm(message);
        };

        return service;
    };

    module.factory("utils", ["loadingService", utils]);

})(angular.module("oisys-app"));