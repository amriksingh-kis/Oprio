// services/authentication.js
define(function (require) {
    var system = require('durandal/system'),
    app = require('durandal/app'),
    router = require('durandal/plugins/router'),
    datacontext = require('services/data');

    return {
        handleUnauthorizedAjaxRequests: function (callback) {
            var self = this;
            self.isAuthenticated(true);
            localStorage.isAuthenticated = true;
            if (!callback) { 
                return;
            }
            $(document).ajaxError(function (event, request, options) {
                if (request.status === 401) {
                    self.isAuthenticated(false);
                    localStorage.isAuthenticated = false;
                    callback();
                }
            });
        },
        isAuthenticated: ko.observable(localStorage.isAuthenticated),
        userId: ko.observable(localStorage.userId),
        userName: ko.observable(localStorage.userName),
        canLogin: function () {
            return true;
        },
        logOff: function () {
            var that = this;
            if (that.isAuthenticated() === true) {
                var logOffPromise = $.ajax({
                    type: "POST",
                    url: "account/logoff",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).
                done(function (response) {
                    if (response.success) {
                        that.isAuthenticated(false);

                        localStorage.isAuthenticated = false;
                        localStorage.userId = null;
                        localStorage.userName = null;

                        datacontext.manager.clear();
                        router.navigateTo('/#/account/login');
                    }
                });
                return logOffPromise;
            }
        },
        login: function (userInfo, navigateToUrl) {
            var that = this;
            if (!this.canLogin()) {
                return system.defer(function (dfd) {
                    dfd.reject();
                }).promise();
            }
            var loginPromise = $.ajax({
                type: "POST",
                url: "/account/logon",
                data: userInfo,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).
                done(function (response) {
                    if (response.Success) {
                        that.userId(parseInt(response.Message, 10));
                        that.userName(response.Name);
                        that.isAuthenticated(true);

                        localStorage.userId = that.userId();
                        localStorage.userName = that.userName();
                        localStorage.isAuthenticated = true;

                        router.navigateTo('/#/conversations/priority');
                    }
                    else {
                        that.isAuthenticated(false);
                        localStorage.isAuthenticated = false;
                    }
                });

            return loginPromise;
            //    var jqxhr = $.post("/account/logon",userInfo,null,"json")
            //        .done(function (data) {
            //            if (data.success == true) {
            //                if (!!navigateToUrl) {
            //                    router.navigateTo(navigateToUrl);
            //                } else {
            //                    return true;
            //                }
            //            } else {
            //                return data;
            //            }
            //        })
            //        .fail(function (data) {
            //            return data;
            //        });

            //    return jqxhr;
        }
    };
});