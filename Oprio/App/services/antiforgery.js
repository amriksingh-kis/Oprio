// services/antiforgery.js
define(function (require) {
    var app = require('durandal/app');

    return {
        /*  this intercepts all ajax requests (with content)
            and adds the MVC AntiForgeryToken value to the data
            so that your controller actions with the [ValidateAntiForgeryToken] attribute won't fail

            original idea came from http://stackoverflow.com/questions/4074199/jquery-ajax-calls-and-the-html-antiforgerytoken

            to use this

            1) ensure that the following is added to your Durandal Index.cshml
            <form id="__AjaxAntiForgeryForm" action="#" method="post">
                @Html.AntiForgeryToken()
            </form>

            2) in  main.js ensure that this module is added as a dependency

            3) in main.js add the following line
            antiforgery.addAntiForgeryTokenToAjaxRequests();

        */
        addAntiForgeryTokenToAjaxRequests: function () {
            var token = $('#__AjaxAntiForgeryForm  input[name=__RequestVerificationToken]').val();
            if (!token) {
                app.showMessage('ERROR: Authentication Service could not find     __RequestVerificationToken');
            }
            var tokenParam = "__RequestVerificationToken=" + encodeURIComponent(token);

            $(document).ajaxSend(function (event, request, options) {
                if (options.hasContent) {
                    request.setRequestHeader('RequestVerificationToken', tokenParam);
                    //options.headers = { 'RequestVerificationToken': tokenParam };
                }
            });
        }

    };
});