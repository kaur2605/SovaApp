define(['knockout', 'app/dataservice', 'app/config'], function (ko, dataservice, config) {
    return function () {
        var questionsdata = ko.observableArray();
        var questionsprev = ko.observable();
        var questionsnext = ko.observable();
        var questionstotal = ko.observable();
        var questionspage = ko.observable('null');
        var pagenumber = ko.computed(function () {
            if (questionspage() !== 'null') {
                return questionspage() + 1;
            } return '';
        });
        var totalPages = ko.observable();
        var questionComponent = ko.observable(config.questionComponent);
        var noElements = ko.computed(function () {
            return questionsdata().length === 0;
        });
        var callback = function (data) {

            questionspage(data.page);
            questionsprev(data.prev);
            questionsnext(data.next);
            questionstotal(data.total);
            questionsdata(data.data);
            totalPages(data.pages);

        };

        dataservice.getMarkings(callback);

        var prevClick = function () {
            dataservice.getQuestions(questionsprev(), callback);
        };
        var nextClick = function() {
            dataservice.getQuestions(questionsnext(), callback);
        };

        var gotoQuestion = function (postUrl, root) {
            postman.notify({ component: config.questionComponent, url: postUrl, prevComponent: root.currentComponent() }, "currentComponent");
        };
    
        return {
      
         prevClick: prevClick,
            nextClick: nextClick,
            prev: questionsprev,
            next: questionsnext,
            total: questionstotal,
            pageNumber: pagenumber,
            gotoQuestion: gotoQuestion,
            questionComponent: questionComponent,
            data: questionsdata,
            noElements,
            totalPages
        
        }
    };
});