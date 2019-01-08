define(['knockout', 'app/dataservice', 'app/config'], function (ko, dataservice, config) {
    return function (params) {
        var answers = ko.observableArray();
        var answersUrl = params.answersUrl;
        var commentsComponent = ko.observable(config.commentsComponent);


        var callback = function(data) {
            answers(data.data);
         
        }

        dataservice.getAnswers(answersUrl, callback);

        return {
           answers: answers,
           commentsComponent: commentsComponent
        }
    };
});