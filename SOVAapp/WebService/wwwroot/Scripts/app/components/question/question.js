define(['knockout', 'app/dataservice', 'app/config'], function (ko, dataservice, config) {
    return function (params) {

        var question = ko.observable();
        var url = ko.observable();
        var commentsComponent = ko.observable(config.commentsComponent);
        var answersComponent = ko.observable(config.answersComponent);
        var annotationsComponent = ko.observable(config.annotationsComponent);
        var body = ko.observable();
        var prevComponent = ko.observable(params.prevComponent);
        var linkedPosts = ko.observableArray();
        var markingStatus = ko.observable();
        var myPostId = ko.observable(params.url);
        var myAnnotationUrl = config.markingsUrl + myPostId(); 

        var doneMarking = ko.computed(function () {
            return markingStatus() === "Already marked";
        });

        var isMarked = ko.computed(function () {
            return markingStatus() === "Already marked";
        });

        var containsElements = ko.computed(function () {
            return linkedPosts().length !== 0
        });

        var QuestionUrl = config.questionsUrl + "/" + myPostId();

        var callback = function (data) {
            question(data);
            body(data.body);
            url(data.url);
            linkedPosts(data.linkedPosts);
            markingStatus(data.markThisPost); 
        }

        dataservice.getQuestion(QuestionUrl, callback);
        
        var markThis = function () {
            var AddMarkingUrl = config.markingsUrl.concat(question().postId);
            var markingObject = ko.toJS({
            });
            dataservice.postData(AddMarkingUrl, markingObject);
            dataservice.getQuestion(QuestionUrl, function (data) {
                markingStatus(data.markThisPost);
            });
        }

        var unMarkThis = function () {
            var deleteMarkingUrl = config.markingsUrl.concat(question().postId);
            var markingObject = ko.toJS({
            });
            dataservice.deleteData(deleteMarkingUrl, markingObject); 
            markingStatus("Deleted marking");
        }

        var goToLinkedPost = function(url) {
            console.log("ok");
            dataservice.getQuestion(url, callback);
        }
        
        var goback = function () {
            postman.notify({ component: prevComponent() }, "currentComponent");
        }

       

        return {
            question: question,
            body: body,
            commentsComponent: commentsComponent,
            answersComponent: answersComponent,
            annotationsComponent: annotationsComponent,
            url: url,
            goback: goback,
            myLinkedPosts: linkedPosts,
            goToLinkedPost: goToLinkedPost,
            markThis,
            markingStatus,
            unMarkThis,
            isMarked,
            containsElements,
            myPostId,
            myAnnotationUrl
       
       
       
        
        }
    };
});