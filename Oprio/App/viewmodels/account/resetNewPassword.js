define(['services/logger', 'durandal/plugins/router'], function (logger, router) {
    function ViewModelA() {
        var self = this;
        self.activate = function (data) {
            debugger;
            activate(data);
        };
        self.title = ko.observable("");
        self.ReNewPassword = ko.observable("");
        self.NewPassword = ko.observable("");
        
        self.ticketID = ko.observable("");
        self.ResetPassword = function () {
            
            var Param = "{'password':'" + self.NewPassword() + "','ticketID':'" + self.ticketID()+ "'}";

                $.ajax({
                    type: "POST",
                    url: "/Account/ResetPassword",
                    data: Param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.Success) {
                            router.navigateTo('/#/account/login');
                            logger.log(response.Message, null, 'home', true);
                        }
                        else {
                            
                            logger.log(response.Message, null, 'home', true);

                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        debugger;
                    alert(xhr.status);
                    alert(thrownError);
                }
                });
        };
    }
    var vm = new ViewModelA();
    return vm;

    //#region Internal Methods

    function activate(data) {
        debugger;
      
        vm.ticketID(data.id);
        //logger.log('Reset New Password', null, 'home', true);
        return true;
    }
    //#endregion
});