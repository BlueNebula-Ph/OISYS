(function (module) {
    module.factory("User", function () {
        function User(id, username, password, firstname, lastname, admin, canView, canWrite, canDelete) {
            this.id = id || 0;
            this.username = username || "";
            this.password = password || "";
            this.firstname = firstname || "";
            this.lastname = lastname || "";
            this.admin = admin || false;
            this.canView = canView || true;
            this.canWrite = canWrite || false;
            this.canDelete = canDelete || false;

            this.accessRights = "";
            this.accessList = [
                { text: "Administrator", value: "admin", selected: this.admin },
                { text: "Can View", value: "canView", selected: this.canView },
                { text: "Can Write", value: "canWrite", selected: this.canWrite },
                { text: "Can Delete", value: "canDelete", selected: this.canDelete }
            ];
        };

        User.prototype = {
            setAccessRights: function () {
                var userAccess = [];
                this.accessList.forEach((val, idx) => {
                    if (val.selected) {
                        userAccess.push(val.value);
                    }
                });

                this.accessRights = userAccess.join(',');
            },
            refreshAccessList: function () {
                this.accessList.forEach((val, idx) => {
                    val.selected = this[val.value];
                });
            }
        };

        return User;
    });

})(angular.module("oisys-app"));