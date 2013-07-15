define(['services/logger', 'services/data', 'durandal/app', 'services/authentication', 'durandal/plugins/router', 'viewmodels/conversations/conversations'], function (logger, data, app, authentication, router, conversations) {
    "use strict";
    function ViewModel() {
        this.filterType = ko.observable(1);
    };

    ViewModel.prototype = conversations;
    ViewModel.prototype.constructor = ViewModel;
    return new ViewModel();
})