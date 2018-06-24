(function (module) {
    var currentUser = function (localStorage, keys, PermPermissionStore) {

        var setUserProfile = function (username, token) {
            userProfile.username = username;
            userProfile.token = token;

            if (token) {
                // Setup other properties from token such as permissions
                var payload = JSON.parse(window.atob(token.split('.')[1]));
                userProfile.userId = payload.sid;
                userProfile.name = payload.given_name;

                if (payload.accessRights) {
                    var accessRights = payload.accessRights.split(',');
                    PermPermissionStore.defineManyPermissions(accessRights, function () {
                        return true;
                    });

                    localStorage.add(keys.userpermissions, accessRights);
                }

                localStorage.add(keys.userkey, userProfile);
            }
        };

        var initialize = function () {
            var user = {
                userId: 0,
                username: "",
                name: "",
                token: "",
                get loggedIn() {
                    return this.token;
                }
            };

            // Get user info from local storage
            var localUser = localStorage.get(keys.userkey);
            if (localUser) {
                user.userId = localUser.userId;
                user.username = localUser.username;
                user.name = localUser.name;
                user.token = localUser.token;
            }

            // Get user permissions from local storage
            var accessRights = localStorage.get(keys.userpermissions);
            if (accessRights) {
                PermPermissionStore.defineManyPermissions(accessRights, function () {
                    return true;
                });
            }

            return user;
        };

        var userProfile = initialize();

        return {
            setUserProfile: setUserProfile,
            userProfile: userProfile
        };
    };

    module.factory("currentUser", ["localStorage", "keys", "PermPermissionStore", currentUser]);

})(angular.module("oisys-app"));