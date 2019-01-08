define(['knockout', 'app/dataservice', 'app/config'], function (ko, dataservice, config) {
    return function (params) {
       
        var words = ko.observableArray();
        var jCloudUrl = params.jCloudUrl;
        var callback = function (data) {
            words(data);
        }
        dataservice.getTermsByPost(jCloudUrl, callback);

        return {
        
            words
            
        };
    }
});