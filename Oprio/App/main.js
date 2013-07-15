require.config({
    paths: { "text": "durandal/amd/text" }
});

define(['durandal/app', 'durandal/viewLocator', 'durandal/system', 'durandal/plugins/router', 'services/logger', 'services/antiforgery', 'services/authentication', 'services/data'],
    function (app, viewLocator, system, router, logger, antiforgery, authentication, data) {

        // Enable debug message to show in the console 
        system.debug(true);

        //patch the promises to use Q
        system.defer = function (action) {
            var deferred = Q.defer();
            action.call(deferred, deferred);
            var promise = deferred.promise;
            deferred.promise = function () {
                return promise;
            };
            return deferred;
        };

        app.start().then(function () {
            toastr.options.positionClass = 'toast-bottom-right';
            toastr.options.backgroundpositionClass = 'toast-bottom-right';

            router.handleInvalidRoute = function (route, params) {
                logger.logError('No Route Found', route, 'main', true);
            };

            router.guardRoute = function guardRoute(routeInfo, params, instance) { 
                if (routeInfo.name == 'Account/login')
                    return true;
                logger.log(authentication.isAuthenticated());
                if (!authentication.isAuthenticated()) {
                    //router.activate('account/login');
                    return '/#/account/login';
                }
                else
                    return true;
            }

            // When finding a viewmodel module, replace the viewmodel string 
            // with view to find it partner view.
            router.useConvention();
            viewLocator.useConvention();

            authentication.handleUnauthorizedAjaxRequests(function () {
                //app.showMessage('You are not authorized, please login')
                //.then(function () {
                router.navigateTo('account/login');
                //});
            });

            //read name, if available
            //var auth = $.cookie('.ASPXAUTH');
            //if (auth) {
            //    logger.log(auth);
            //}

            antiforgery.addAntiForgeryTokenToAjaxRequests();

            data.activate();
            // Adapt to touch devices
            app.adaptToDevice();
            //Show the app by setting the root view model for our application.
            app.setRoot('viewmodels/shell', 'entrance');
        });
    });