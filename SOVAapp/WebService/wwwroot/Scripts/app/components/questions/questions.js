define(['knockout', 'app/dataservice', 'app/config'], function (ko, dataservice, config) {
    return function (params) {
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

        var url = params.url;
        var myPrevComponent = params.prevComponent;
        console.log(myPrevComponent);

        var isTherePrev = function () {
            if (myPrevComponent === undefined) {
                return false;
            }
            return true;
        }
     
        var callback = function (data) {

            questionspage(data.page);
            questionsprev(data.prev);
            questionsnext(data.next);
            questionstotal(data.total);
            questionsdata(data.data);
            totalPages(data.pages);
        };

        dataservice.getQuestions(url,callback);

        var prevClick = function () {
            questionsdata([]);
            dataservice.getQuestions(questionsprev(), callback);
        };
        var nextClick = function () {
            questionsdata([]);
            dataservice.getQuestions(questionsnext(), callback);
        };

        var gotoquestion = function (postId, root) {
            postman.notify({ component: config.questionComponent, url: postId, prevComponent: root.currentComponent() }, "currentComponent");
        };
        var goback = function () {
            postman.notify({ component: myPrevComponent }, "currentComponent");
        }

        return {
            prevClick: prevClick,
            nextClick: nextClick,
            prev: questionsprev,
            next: questionsnext,
            total: questionstotal,
            pageNumber: pagenumber,
            gotoquestion: gotoquestion,
            questionComponent: questionComponent,
            data: questionsdata,
            noElements,
            totalPages, 
            goback,
            isTherePrev
        }
    };
});