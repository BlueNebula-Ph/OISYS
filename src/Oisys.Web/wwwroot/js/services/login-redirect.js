(function (module) {

    var loginRedirect = function ($q, $location) {

        var lastPath = "/";

        var responseError = function (response) {
            if (response.statusCode == 401) {
                lastPath = $location.path();
                $location.path("/login");
            }
            return $q.reject(response);
        };

        var redirectPostLogin = function () {
            $location.path(lastPath);
            lastPath = "/";
        };

        return {
            redirectPostLogin: redirectPostLogin,
            responseError: responseError
        };
    };

    module.factory("loginRedirect", ["$q", "$location", loginRedirect]);
    module.config(function ($httpProvider) {
        $httpProvider.interceptors.push("loginRedirect");
    });

})(angular.module("oisys-app"));