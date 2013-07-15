define(['services/logger', 'services/data', 'durandal/app', 'services/authentication', 'durandal/plugins/router'], function (logger, data, app, authentication, router) {
    function ViewModel() {
        var self = this;
        this.activate = activate;
        this.title = 'Conversations';
        this.conversations = ko.observableArray();
        this.userId = authentication.userId;
        this.archive = archive,
        this.pin = pin,
        this.name = authentication.userName;
        this.items = ko.observableArray();
        this.filterType = ko.observable(1);
        this.showPanel = showPanel;
        this.hidePanel = hidePanel;
        this.select = select;
        this.up = up;
        this.down = down;

    };

    var vm = new ViewModel();
    var promises = [];
    var deferred = Q.defer();
    return vm;


    function up(data, event) {
        var current = $('.items .selected');
        if (current.length == 0) {
            $('.items >  div:first-child').addClass('selected');
            current = $('.items .selected');
        }
        select(data, current.prev().before(current));
    }

    function down(data, event) {

        var current = $('.items .selected');
        if (current.length == 0) {
            $('.items >  div:first-child').addClass('selected');
            current = $('.items .selected');
        }
        select(data, current.next().after(current));
    }

    function select(data, event) {
        var $this = $(event.target);
        if ($this.siblings('div').size() == 0) {
            $this = $(event.target).parent('div')
        }
        $this.siblings().removeClass('selected');
        $this.addClass('selected');

    }

    function showPanel() {
        $('.before-title').css('margin-left', '310px');
        $('.detail-title').css('margin-left', '310px');
        $('.detail-items').css('margin-left', '210px');
        $('.expand-panel').show();
        $('.contract-panel').hide();
        $('.items >  div:first-child').addClass('selected');
    }

    function hidePanel() {
        $('.before-title').css('margin-left', '200px');
        $('.detail-title').css('margin-left', '200px');
        $('.detail-items').css('margin-left', '100px');
        $('.expand-panel').hide();
        $('.contract-panel').show();
    }

    function activate(r) {
        return init(r);
    }

    function init(r) {
        var id = parseInt(r.id);
        return data.getConvById(id)
                .then(succeeded)
                .then(function () {

                    Q.all(promises).done(function () {

                        logger.logError("Done");
                        return deferred.resolve(true);
                    });
                    return deferred.promise;
                })
             .then(function () {
                 logger.logError("Done1");
             })
               .fail(failed);
    }

    function succeeded(d) {
        promises = [];
        vm.conversations.removeAll();
        var i = d.results[0];
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
        i.lastmessage = ko.observableArray();
        i.messageformattedTimeStamp = ko.observable("");

        getCounts(i);
        getLastMessage(i);

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
        promises.push(p);
        return p;
    }
    function getLastMessage(item) {
        var p = data.getLastMessageOfConv(item.id()).then(function (message) {
            item.lastmessage.removeAll();
            item.lastmessage(message.results[0]);
            var dt = message.results[0].creationTimestamp();
            var formatedDate = dt.getMonthNameShort() + " " + dt.getDate() + ", " + dt.getFullYear();
            item.messageformattedTimeStamp(formatedDate);
        })
        .then(function (r) {
            vm.conversations.push(item);
        })
        .fail(function (error) {
            logger.logError(error);
        });
        promises.push(p);
        return p;
    }

    function failed(error) {
        logger.logError(error.message, "Query Failed!")
    }

    function pin(conv) {
        conv.isPinned(true);
        data.saveConversation(conv);
    }

    function archive(conv) {
        conv.isArchived(true);
        data.saveConversation(conv);
    }

    function filterItems(tagPerson, type) {
        var result = {
            total: 0,
            unread: 0
        };
        tagPerson.tagPersonItems().forEach(function (i) {
            if (i.item().itemType().name() == type) {
                result.total = result.total + 1;
                if (i.isViewed() != true) {
                    result.unread = result.unread + 1;
                }
            }
        });

        return result;
    }


    //#endregion
});