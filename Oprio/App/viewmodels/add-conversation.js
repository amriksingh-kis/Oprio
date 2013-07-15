define(['services/logger', 'services/data', 'services/authentication', 'durandal/plugins/router'], function (logger, datacontext, authentication, router) {
    //#region Internal Methods
    var
        _conversationName = ko.observable(""),
        _message = ko.observable(""),
        _people = ko.observableArray(),


        _addConversation = function () {
            var creatorId = authentication.userId();


            var conv, item, tagPerson, itemTypeId, itemStatus;

            datacontext
                .getItemType("Message")
                .then(function (data) {
                    if (data.results.length > 0) {
                        itemTypeId = data.results[0].id();
                        return createConversation();
                    }
                    else {
                        logger.log('No ItemType results');
                    }
                })
                .then(createTagPerson)
                .then(createItem)
                .then(createTagPersonItem)
               // .then(createItemStatus)
                .then(createItemStatusHistory)
                .then(finishCreationProcess);



            function createConversation() {
                conv = datacontext.createConversation();
                conv.name(_conversationName());
                conv.creationTimestamp(new Date());
                conv.creatorPersonID(creatorId);
                return datacontext.saveConversation(conv);
            }

            function createTagPerson() {
                tagPerson = datacontext.createTagPerson()
                tagPerson.tagID(conv.id());
                tagPerson.personID(creatorId);
                tagPerson.creatorPersonID(creatorId);
                tagPerson.creationTimestamp(new Date());
                tagPerson.lastAccessed(new Date());
                return datacontext.saveTagPerson(tagPerson)
            }

            function createItem() {

                item = datacontext.createItem();
                item.itemText(_message());
                item.creatorPersonID(creatorId);
                item.itemTypeID(itemTypeId);
                item.creationTimestamp(new Date());
                item.modifiedTimestamp(new Date());
                return datacontext.saveItem(item);
            }

            function createTagPersonItem() {
                var tagPersonItem = datacontext.createTagPersonItem({ tagPersonID: tagPerson.id(), itemID: item.id() });
                tagPersonItem.creatorPersonID(creatorId);
                tagPersonItem.creationTimeStamp(new Date());
                datacontext.saveTagPersonItem(tagPersonItem);
            }

            //function createItemStatus() {
            //    itemStatus = datacontext.createItemStatus();
            //    itemStatusHistory.name("New");
            //    itemStatusHistory.itemTypeID(itemTypeId);               
            //    datacontext.saveItemStatus(itemStatus);
            //}

            function createItemStatusHistory()
            {
                var itemStatusHistory = datacontext.createItemStatusHistory();
                itemStatusHistory.itemID(item.id());
                itemStatusHistory.item(item);
                itemStatusHistory.itemStatusID(8);
                itemStatusHistory.setAtTimestamp(new Date());
                itemStatusHistory.setByPersonID(creatorId);
                datacontext.saveItemStatusHistory(itemStatusHistory);
            }

            function finishCreationProcess() {
                _message("");
                _conversationName("");
                router.navigateTo("#/conversations/priority");
            }

            // TO DO Create TagPerson for every shared person

        },
        _activate = function () {
            return true;
        };
    //#endregion

    return {
        activate: _activate,
        addConversation: _addConversation,
        title: 'Add Conversation',
        conversationName: _conversationName,
        message: _message,
        people: _people
    };
});