define(['services/logger', 'durandal/plugins/router'], function (logger, router) {
  
    function ViewModel() {
        var self = this;
        self.activate = function (data) {
            activate(data);
        };
        self.title = "Request Password change";
        self.ReNewPassword = ko.observable("");
        self.NewPassword = ko.observable("");
        self.checkCode = function (id) {
            
            var Param = "{'id':'" +  id + "'}";

                $.ajax({
                    type: "POST",
                    url: URL + "/Account/ResetPasswordValidate",
                    data: Param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.Success) {
                            
                            router.navigateTo('/#/Account/resetNewPassword/'+id);
                            //logger.log(response.Message, null, 'home', true);
                        }
                        else {
                            //router.navigateTo('/#/login');
                            self.title("Access denied");
                            logger.log(response.Message, null, 'home', true);

                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
                });
        };
    }
    var vm = new ViewModel();
    return vm;

    //#region Internal Methods

    function activate(data) {
        vm.checkCode(data.id);
        //logger.log('Reset Password', null, 'home', true);
        return true;
    }
    //#endregion
});