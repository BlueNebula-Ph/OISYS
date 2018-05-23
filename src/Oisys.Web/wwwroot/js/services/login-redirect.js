(function (module) {

    var loginRedirect = function ($q, $state) {

        var lastState = "home";

        var setLastState = function (newState) {
            lastState = newState;
        };

        var redirectPostLogin = function () {
            $state.go(lastState);
            setLastState("home");
        };

        return {
            setLastState: setLastState,
            redirectPostLogin: redirectPostLogin
        };
    };

    module.factory("loginRedirect", ["$q", "$state", loginRedirect]);

})(angular.module("oisys-app"));