define(['knockout', 'app/dataservice', 'app/config'], function(ko, dataservice, config) {
    return function () {
       
        var postlimit = ko.observable();
        var favortietags = ko.observableArray();
        var recommendedQuestions = ko.observableArray();
        var noElements = ko.computed(function () {
            return recommendedQuestions().length === 0;
        });

        var callback = function (data) {
            postlimit(data.postLimit);
            favortietags(data.favortieTags);
        }

        var callback2 = function (data) {
            recommendedQuestions(data.recommendedQuestions);
        };


    
        dataservice.getCustomefield(callback);


        dataservice.getFavoriteQuestions(callback2);
        var gotoquestion = function (PostId, root) {
            postman.notify({ component: config.questionComponent, url: PostId, prevComponent: root.currentComponent() }, "currentComponent");
        };

        return {
           
            gotoquestion: gotoquestion,
            recommendedQuestions: recommendedQuestions,
            noElements,
            postlimit: postlimit,
            favortietags: favortietags,

        }
    }
});