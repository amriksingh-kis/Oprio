define(['durandal/system', 'durandal/plugins/router', 'services/logger', 'services/authentication'],
    function (system, router, logger, authentication) {
        var shell = {
            activate: activate,
            router: router,
            token: $("input[name='__RequestVerificationToken']").val(),
            isAuthenticated: ko.computed(function(){ return authentication.isAuthenticated()}),
            navSelect: navSelect,
            logOff: logOff,
            userName: ko.computed(function(){return authentication.userName()})
        };
        
        return shell;

        //#region Internal Methods
        function navSelect(item, data) {
            $('.active').removeClass('active');
            $(data.target).parent('a').addClass('active');
            return true;
        }
        function logOff() {
            return authentication.logOff();
        }
        
        function activate() {
            return boot();
        }

        function boot() {
           router.mapNav('account/login'); 
           router.mapNav('account/resetPassword/:id');
           router.mapNav('add-conversation');
           router.mapNav('conversations/:filter', 'viewmodels/conversations/conversations');
           //router.mapNav('conversations/priority', 'viewmodels/conversations/conversations');
           //router.mapNav('conversations/time', 'viewmodels/conversations/conversations');
           //router.mapNav('conversations/archived', 'viewmodels/conversations/conversations');
           router.mapNav('conversationdetail/:id');
          
           return router.activate('conversations/priority');
        }

        function log(msg, data, showToast) {
            logger.log(msg, data, system.getModuleId(shell), showToast);
        }
        //#endregion
    });