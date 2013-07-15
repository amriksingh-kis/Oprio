define(['services/logger', 'services/authentication'], function (logger, authentication) {
    configureBreeze();
    var serviceName = 'api/oprio';
    var Predicate = breeze.Predicate;
    var manager = new breeze.EntityManager(serviceName);
    //manager.enableSaveQueuing(true);
    configureManagerToSaveModifiedItemImmediately();
  

    var datacontext = {
        manager: manager,
        activate: function () {
            //call at start
            return manager.fetchMetadata();
        },
        metadataStore: manager.metadataStore,
        // getTodoLists: getTodoLists,
        createTag: createTag,
        createConversation: createConversation,
        createTagPerson: createTagPerson,
        createItem: createItem,
        createTagPersonItem: createTagPersonItem,
        createItemStatusHistory: createItemStatusHistory,
        createItemStatus:createItemStatus,
        //createTodoItem: createTodoItem,
        saveConversation: saveConversation,
        saveTagPerson: saveTagPerson,
        saveItem: saveItem,
        saveItemStatusHistory: saveItemStatusHistory,
        saveItemStatus:saveItemStatus,
        getItemType: getItemType,
        getItemCounts: getItemCounts,
        saveTagPersonItem: saveTagPersonItem,
        getConvs: getConvs,
        getConvById: getConvById,
        getTagPersonByConvId: getTagPersonByConvId,
        getTagPersonItemsByTagPersonId: getTagPersonItemsByTagPersonId,
        getItemById: getItemById,
        getLastItemOfConv: getLastItemOfConv,
        getItems: getItems

        
        //saveNewTodoList: saveNewTodoList,
        //deleteTodoItem: deleteTodoItem,
        //deleteTodoList: deleteTodoList
    };

    //model.initialize(datacontext);
    return datacontext;

    //#region Private Members
    function getConvs(filterType)
    {
        var pred = breeze.Predicate.create("tag.isConversation", "==", true);
        if (filterType == 1)
        {
            pred = pred.and("pinned", "==", null).and("archived", "==", null)
        }
        else if (filterType == 3) {
            pred = pred.and("archived", "!=", null)
        }

        //var query = breeze.EntityQuery.from('Tags').where(pred).orderByDesc("creationTimestamp").expand("person");
        var query = breeze.EntityQuery.from('TagPersons').where(pred).orderByDesc("tag.creationTimestamp").expand("tag.person");
        //var query = breeze.EntityQuery.from('Tags').where(pred).orderByDesc("creationTimestamp").expand("tagPersons, tagPersons.person");
        return manager.executeQuery(query);
    }

   
    function getItemCounts(convId) {
        var pred = breeze.Predicate.create("tagPerson.tag.isConversation", "==", true).and("tagPerson.tagID", "==", convId);
        //var query = breeze.EntityQuery.from('TagPersons').where(pred).expand("tag, tagPersonItems").take(0).inlineCount(true);
        //var query = breeze.EntityQuery.from('Items').where(pred).expand("tagPerson.tag").take(0).inlineCount(true);
        var query = breeze.EntityQuery.from('TagItemCount').withParameters({tagId: convId});
        return manager.executeQuery(query);
    }

    function getConvById(Id) {
        var query = breeze.EntityQuery.from('Tags').where("id", "==", Id).expand("Person");
        return manager.executeQuery(query);
    }

    function getItemType(name) {
        var query = breeze.EntityQuery
            .from('ItemType')
            .where("Name", "==", name);//.orderByDesc('CreationTimestamp');
       return manager.executeQuery(query);
    }

    function getCurrentUserId() {

    }

    function getTagPersonByConvId(id) {
        var query = breeze.EntityQuery.from('TagPersons').where("tagID", "==", id);
        return manager.executeQuery(query);
    }

    function getTagPersonItemsByTagPersonId(id)
    {
        var query = breeze.EntityQuery.from('TagPersonItems').where("tagPersonID", "==", id);
        return manager.executeQuery(query);
    }

    function getItemById(id) {
        var query = breeze.EntityQuery.from('Items').where("id", "==", id);
        return manager.executeQuery(query);
    }

    function getLastItemOfConv(convId) {
        var query = breeze.EntityQuery.from('GetLastItem').withParameters({ tagId: convId });
        return manager.executeQuery(query);
    }

    function getItems(convId) {
        var query = breeze.EntityQuery.from('GetItems').withParameters({ tagId: convId });
        return manager.executeQuery(query);
    }

    

    function createTag() {
        return manager.createEntity("Tag");
    }

    function createTagPerson() {
        return manager.createEntity("TagPerson");
    }

    function createItem() {
        return manager.createEntity("Item");
    }

    function createTagPersonItem(constructorData) {
        return manager.createEntity("TagPersonItem", constructorData);
    }

    function createItemStatusHistory() {
        return manager.createEntity("ItemStatusHistory");
    }

    function createItemStatus() {
        return manager.createEntity("ItemStatus");
    }

    function createConversation() {
        var conv = createTag();
        conv.isConversation(true);
        return conv;
    } 

    function saveConversation(conversation) {
        return saveEntity(conversation);
    }

    function saveTagPerson(tagPerson) {
        return saveEntity(tagPerson);
    }

    function saveItem(item) {
        return saveEntity(item);
    }

    function saveTagPersonItem(tagPersonItem) {
        return saveEntity(tagPersonItem);
    }

    function saveItemStatusHistory(itemStatusHistory) {
        return saveEntity(itemStatusHistory);
    }

    function saveItemStatus(itemStatus) {
        return saveEntity(itemStatus);
    }


    //function getTodoLists(todoListsObservable, errorObservable) {
    //    return breeze.EntityQuery
    //        .from("TodoLists").expand("Todos")
    //        .orderBy("todoListId desc")
    //        .using(manager).execute()
    //        .then(getSucceeded)
    //        .fail(getFailed);

    //    function getSucceeded(data) {
    //        todoListsObservable(data.results);
    //    }

    //    function getFailed(error) {
    //        errorObservable("Error retrieving todo lists: " + error.message);
    //    }
    //}

    //function createTodoItem() {
    //    return manager.createEntity("TodoItem");
    //}



    //function saveNewTodoList(todoList) {
    //    return saveEntity(todoList);
    //}

    //function deleteTodoItem(todoItem) {
    //    todoItem.entityAspect.setDeleted();
    //    return saveEntity(todoItem);
    //}

    //function deleteTodoList(todoList) {
    //    // Neither breeze nor server cascade deletes so we have to do it
    //    var todoItems = todoList.todos().slice(); // iterate over copy
    //    todoItems.forEach(function (entity) { entity.entityAspect.setDeleted(); });
    //    todoList.entityAspect.setDeleted();
    //    return saveEntity(todoList);
    //}

    function saveEntity(masterEntity) {

        return manager.saveChanges().fail(saveFailed);

        function saveFailed(error) {
            var msg = "Error saving " +
                describeSaveOperation(masterEntity) + ": " +
                getErrorMessage(error);

            masterEntity.errorMessage(msg);
            // Let user see invalid value briefly before reverting
            setTimeout(function () { manager.rejectChanges(); }, 1000);
            throw error; // so caller can see failure
        }
    }

    function describeSaveOperation(entity) {
        var statename = entity.entityAspect.entityState.name.toLowerCase();
        var typeName = entity.entityType.shortName;
        var title = entity.title && entity.title();
        title = title ? (" '" + title + "'") : "";
        return statename + " " + typeName + title;
    }
    function getErrorMessage(error) {
        var reason = error.message;
        if (reason.match(/validation error/i)) {
            reason = getValidationErrorMessage(error);
        }
        return reason;
    }
    function getValidationErrorMessage(error) {
        try { // return the first error message
            var firstItem = error.entitiesWithErrors[0];
            var firstError = firstItem.entityAspect.getValidationErrors()[0];
            return firstError.errorMessage;
        } catch (e) { // ignore problem extracting error message 
            return "validation error";
        }
    }

    function configureBreeze() {
        // configure to use camelCase
        breeze.NamingConvention.camelCase.setAsDefault();

        // configure to resist CSRF attack
        //var antiForgeryToken = $("#__AjaxAntiForgeryForm  input[name=__RequestVerificationToken]").val();
        //if (antiForgeryToken) {
        //    // get the current default Breeze AJAX adapter & add header
        //    var ajaxAdapter = breeze.config.getAdapterInstance("ajax");
        //    ajaxAdapter.defaultSettings = {
        //        headers: {
        //            'RequestVerificationToken': antiForgeryToken
        //        },
        //        data: {
        //            '__RequestVerificationToken': antiForgeryToken
        //        }
        //    };
        //}
    }
    function configureManagerToSaveModifiedItemImmediately() {
        manager.entityChanged.subscribe(entityStateChanged);

        function entityStateChanged(args) {
            if (args.entityAction === breeze.EntityAction.EntityStateChange) {
                var entity = args.entity;
                if (entity.entityAspect.entityState.isModified()) {
                    saveEntity(entity);
                }
            }
        }
    }
    //#endregion
});