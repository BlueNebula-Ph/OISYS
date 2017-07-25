(function (module) {

    var tokenService = function (currentUser, $q) {

        var request = function (config) {
            if (currentUser.userProfile.loggedIn) {
                config.headers.Authorization = "Bearer " + currentUser.userProfile.token;
            }
            return $q.when(config);
        };

        return {
            request: request
        };

    };

    module.factory("tokenService", ["currentUser", "$q", tokenService]);
    module.config(function ($httpProvider) {
        $httpProvider.interceptors.push("tokenService");
    });

})(angular.module("oisys-app"));