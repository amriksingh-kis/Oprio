define(['services/logger', 'services/data', 'durandal/app'], function (logger, data, app) {
    function ViewModel() {
        var self = this;
        this.activate = activate;       
        this.select = select;
    };

    var vm = new ViewModel();
    return vm;

    
    function select(item) {
        logger.logger("Selected")
    }
    //#region Internal Methods
    function activate() {
        return true;
    }
    //#endregion
});