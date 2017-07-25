(function (module) {

    var currentUser = function () {

        var setUserProfile = function (username, token) {
            userProfile.username = username;
            userProfile.token = token;
        };

        var userProfile = {
            username: "",
            token: "",
            get loggedIn() {
                return this.token;
            }
        };

        return {
            setUserProfile: setUserProfile,
            userProfile: userProfile
        };

    };

    module.factory("currentUser", [currentUser]);

})(angular.module("oisys-app"));