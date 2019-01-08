define(['knockout', 'app/dataservice', 'app/config'], function (ko, dataservice, config) {
    return function (params) {
        $("#functionalSearchBar").hide();
        var applicationName = config.applicationName;

        var currentComponent = ko.observable();

        var isMenuSelected = function(content) {
            return content && currentComponent() === content.toLowerCase();
        };
        
        var changeContent = function (content) {
            if (content !== undefined) {
                    currentComponent(content.toLowerCase());
                    postman.notify({ component: content.toLowerCase() }, "currentComponent");
            }
        };

        changeContent(config.defaultMenu);

       



        return {
            menuElements: config.menuElements,
            currentComponent: currentComponent,
            changeContent: changeContent,
            isMenuSelected: isMenuSelected,
            applicationName: applicationName,
            defaultComponent: config.defaultMenuItem
        }
    };
});