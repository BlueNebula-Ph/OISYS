(function (module) {

    var authService = function ($http, formEncode, currentUser, env) {

        var login = function (username, password) {

            var config = {
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            };

            var data = formEncode({
                username: username,
                password: password,
                grant_type: "password"
            });

            return $http
                .post(env.baseUrl + "/auth/connect/token", data, config)
                .then(function (response) {
                    currentUser.setUserProfile(username, response.data.token);
                    return username;
                });
        };

        return {
            login: login
        };
    };

    module.factory("authService", ["$http", "formEncode", "currentUser", "env", authService]);

})(angular.module("oisys-app"));