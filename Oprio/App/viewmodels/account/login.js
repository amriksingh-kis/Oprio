define(['services/logger', 'durandal/plugins/router', 'services/authentication'], function (logger,router, authentication) {

    function ViewModel()
    {
        var self = this;
        self.activate = function () {
            activate();
        };
        self.title = ko.observable("Login to Oprio ");
        self.UserName = ko.observable("");
        self.Password = ko.observable("");
        self.Email = ko.observable("");
        //self.fullName = ko.computed(function () {
        //    return this.userName() + " " + this.password();
        //}, this);
        self.RememberMe = ko.observable(false);
        self.ShowForget = ko.observable(true);
        
        self.signIn = function () {
            var Param = "{'userInfo':'" + ko.toJSON(self) + "'}";
            return authentication.login(Param); //promise
        };
        
        self.ForgotClick = function () {       
            self.ShowForget(true);
            self.title('Reset Password');
            $('.login-form').hide('slide', {direction:'left'}, 200, function () {
                $('.forgot-pwd-form').show('slide', { direction: 'right' },200, function () {
                    $('.email-txtbox').focus();
                });
            });
        };

        self.BackToLogin = function () {
            self.ShowForget(false);
            self.title('Login to Oprio');
            $('.forgot-pwd-form').hide('slide', { direction: 'right' }, 200, function () {
                $('.login-form').show('slide', { direction: 'left' }, 200, function () {
                    $('.login-txtbox').focus();
                });
            });
        };


        self.SubmitResetPwd = function () {
           // router.navigateTo('/#/home');
            var Param = "{'email':'" + ko.toJSON(self) + "'}";
            
            $.ajax({
                type: "POST",
                url: URL + "Account/ResetPasswordRequest",
                data: Param,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.Success) {
                        
                        logger.log(response.Message, null, 'home', true);

                        //$("#ProfileInformationSection").hide();
                    }
                    else {
                        logger.log(response.Message, null, 'home', true);

                    }

                }
            });
        };
    }
    
    ko.bindingHandlers.slideVisible = {
        init: function (element, valueAccessor) {
            // Initially set the element to be instantly visible/hidden depending on the value
            var value = valueAccessor();
            $(element).toggle(ko.utils.unwrapObservable(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
        },
        update: function (element, valueAccessor) {
            // Whenever the value subsequently changes, slide in or out
            var value = valueAccessor();
            ko.utils.unwrapObservable(value) ? $(element).fadeIn() : $(element).fadeOut();
        }
    };


    var vm = new ViewModel();
    return vm;

    //#region Internal Methods
    function activate() {
        vm.UserName("");
        vm.Password("");
        vm.Email("");
        vm.RememberMe(false);
        vm.ShowForget(false);
//        logger.log('Login View Activated', null, 'home', true);
        return true;
    }
    //#endregion
});