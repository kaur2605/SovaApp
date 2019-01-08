define(['knockout', 'app/config'], function (ko, config) {
        return (function () {
        var currentComponent = ko.observable(config.defaultMenuItem);
        var paramsData = ko.observable();

        postman.notify(currentComponent, "currentComponent");

        
        postman.subscribe(function (value) {
            currentComponent(value.component);
            paramsData({ url: value.url, prevComponent: value.prevComponent});
        }, "currentComponent");

        return {
            currentComponent: currentComponent,
            menuComponent: config.menuComponent,
            paramsData: paramsData,
        }   
    });
});