define(['services/logger', 'services/data', 'durandal/app', 'services/authentication', 'durandal/plugins/router'], function (logger, data, app, authentication, router) {
    function ViewModel() {
        var self = this;
        this.activate = activate;
        this.deactivate = function () {
            self.conversations([]);
        };
        this.title = 'Conversations';
        this.conversations = ko.observableArray([]);
        this.userId = ko.computed(function () { return authentication.userId; });
        this.archive = archive;
        this.pin = pin;
        this.name = ko.computed(function () { return authentication.userName; });
        this.items = ko.observableArray();
        this.togglePriority = togglePriority;
        this.toggleTime = toggleTime;
        this.toggleArchived = toggleArchived;
        this.filterType = ko.observable(1);

        this.gotoDetails = gotoDetails;
        this.isPriorityFilter = ko.computed(function () {
            if (self.filterType() == 1)
            { return true; }
            else
            { return false; }
        });
        this.isTimeFilter = ko.computed(function () {
            if (self.filterType() == 2)
            { return true; }
            else
            { return false; }
        });
        this.isArchiveFilter = ko.computed(function () {
            if (self.filterType() == 3)
            { return true; }
            else
            { return false; }
        });
        this.hideCard = function (elem) {
            if (elem.nodeType === 1) {
                $(elem).fadeOut(400, function () {
                    $(elem).remove();
                });
            }
        }
    }

    var vm = new ViewModel();
    var promises = [];
    return vm;


    function gotoDetails(data, e) {
        if (e.target.nodeName === 'A') return;
        var url = '#/conversationdetail/' + data.id();
        router.navigateTo(url);
    }

    function activate(r) {
        //switch (r.routeInfo.url) {
        //    case 'conversations/priority': vm.filterType(1); break;
        //    case 'conversations/time': vm.filterType(2); break;
        //    case 'conversations/archived': vm.filterType(3); break;
        //}

        vm.filterType(getFilterValue(r.filter));
        return init();
    }

    function getFilterValue(name) {
        switch (name) {
            case 'priority': return 1;
            case 'time': return 2;
            case 'archived': return 3;
        }
    }

    function init() {
        return data.getConvs(vm.filterType())
                .then(succeeded)
                .then(function () {
                    var deferred = Q.defer();
                    Q.all(promises).done(function () {
                        logger.logError("Done");
                        //vm.conversations(tmpConvs);
                        return deferred.resolve();
                    });
                    return deferred.promise;
                })
               .fail(failed);
    }

    function succeeded(d) {
        promises = [];
        vm.conversations([]);
        d.results.forEach(function (item) {
            var i = item.tag();
            i.formattedTimeStamp = ko.computed(function () {
                var dt = i.creationTimestamp();
                return dt.getMonthNameShort() + " " + dt.getDate() + ", " + dt.getFullYear();
            });
            i.creatorFullName = ko.computed(function () {
                return i.person().firstName() + ' ' + i.person().lastName()[0];
            });

            //counts
            i.totalMessages = ko.observable(0);
            i.unreadMessages = ko.observable(0);
            i.totalTrackables = ko.observable(0);
            i.unreadTrackables = ko.observable(0);
            i.totalFiles = ko.observable(0);
            i.unreadFiles = ko.observable(0);
            i.lastItem = ko.observableArray();
            i.lastItemModified = ko.observable("");
            i.isPinned = ko.observable(typeof item.pinned() === 'undefined' ? false : true);
            i.isDeferred = ko.observable(typeof item.deferred() === 'undefined' ? false : true);
            i.isArchived = ko.observable(typeof item.archived() === 'undefined' ? false : true);
            i.tp = item;

            promises.push(getMore(i));
        });
    }

    function getMore(item) {
        return getCounts(item)
            .then(getLastItem(item))
            .then(vm.conversations.push(item));
    }
    function getCounts(item) {
        var p = data.getItemCounts(item.id()).then(function (c) {
            var results = c.results;
            results.forEach(function (r) {
                switch (r.type) {
                    case 1:
                        item.totalMessages(item.totalMessages() + r.count);
                        if (!r.isViewed) {
                            item.unreadMessages(r.count);
                        }
                        break;
                    case 2:
                        item.totalTrackables(item.totalTrackables() + r.count);
                        if (!r.isViewed) {
                            item.unreadTrackables(r.count);
                        }
                        break;
                    case 3:
                        item.totalFiles(item.totalFiles() + r.count);
                        if (!r.isViewed) {
                            item.unreadFiles(r.count);
                        }
                        break;
                }
            });
        })
        .fail(function (error) {
            logger.logError(error);
        });
        return p;
    }
    function getLastItem(item) {
        var p = data.getLastItemOfConv(item.id()).then(function (i) {
            item.lastItem.removeAll();
            item.lastItem(i.results[0]);
            var dt = i.results[0].creationTimestamp();
            var formattedDate = dt.getMonthNameShort() + " " + dt.getDate() + ", " + dt.getFullYear();
            item.lastItemModified(formattedDate);
        })
        .fail(function (error) {
            logger.logError(error);
        });
        return p;
    }

    function failed(error) {
        logger.logError(error.message, "Query Failed!");
    }

    function pin(conv) {
        if (conv.isPinned()) {
            conv.tp.pinned(null);
            conv.isPinned(false);
        }
        else {
            conv.tp.pinned(new Date());
            conv.isPinned(true);
            if (vm.isPriorityFilter())
                vm.conversations.remove(conv);
        }
        data.saveTagPerson(conv.tp);
    }

    function archive(conv) {
        if (conv.isArchived()) {
            conv.tp.archived(null);
            conv.isArchived(false);
            if (vm.isArchiveFilter())
                vm.conversations.remove(conv);
        }
        else {
            conv.tp.archived(new Date());
            conv.isArchived(true);
            if (vm.isPriorityFilter())
                vm.conversations.remove(conv);
        }

        data.saveTagPerson(conv.tp);
    }

    function cleanBindings() {
        var convs = $('#convs')[0];
        ko.cleanNode(convs);
    }

    function togglePriority() {
        cleanBindings();
        router.navigateTo('#/conversations/priority');
    }
    function toggleTime() {
        cleanBindings();
        router.navigateTo('#/conversations/time');
    }
    function toggleArchived() {
        cleanBindings();
        router.navigateTo('#/conversations/archived');
    }
    //#endregion
});