(function () {
    requirejs.config({
        baseUrl: 'Scripts',
        paths: {
            knockout: 'lib/knockout-3.4.0.debug',
            jquery: 'lib/jquery-2.2.3.min',
            text: 'lib/text',
            bootstrap: 'lib/bootstrap.min',
            modernizer: 'lib/modernizr-2.8.3',
            jqcloud: 'lib/jqcloud2/dist/jqcloud.min',
            d3: 'lib/d3js',
            helpers: 'lib/helpers'

        },

        // Explicitly specify that bootstrap is dependant on jquery to avoid dependency errors
        shim: {
            bootstrap: { deps: ['jquery'] },
            jqcloud: { deps: ['jquery'] },
            helpers: { deps: ['jquery'] }


        }
    });
})();

require(['knockout', 'jquery', 'jqcloud'], function (ko, $) {
    ko.bindingHandlers.cloud = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called when the binding is first applied to an element
            // Set up any initial state, event handlers, etc. here
            var words = allBindings.get('cloud').words;
            if (words && ko.isObservable(words)) {
                words.subscribe(function () {
                    $(element).jQCloud('update', ko.unwrap(words));
                });
            }
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called once when the binding is first applied to an element,
            // and again whenever any observables/computeds that are accessed change
            // Update the DOM element based on the supplied values here.
            var words = ko.unwrap(allBindings.get('cloud').words) || [];
            $(element).jQCloud(words);
        }
    };
}); 



postman = {
    subscribers: [],
    subscribe: function (callback, topic, source) {
        var found = false;
        for (var i = 0; i < this.subscribers.length; i++) {
            if (this.subscribers[i].source === source && this.subscribers[i].topic === topic) {
                found = true;
                this.subscribers[i].callback = callback;
            }
        }
        if (!found) {
            this.subscribers.push({ topic: topic, callback: callback, source: source });
        }
    },
    notify: function (value, topic) {
        for (var i = 0; i < this.subscribers.length; i++) {
            if (this.subscribers[i].topic === topic) {
                this.subscribers[i].callback(value);
            }
        }
    }
};

require(['knockout', 'app/viewmodel', 'app/config', 'jquery', 'bootstrap'],
    function (ko, viewmodel, config, $) {

        // Top bar menu
        ko.components.register(config.menuComponent, {
            viewModel: { require: 'app/components/topbarmenu/topbarmenu' },
            template: { require: 'text!app/components/topbarmenu/topbarmenuView.html' }
        });

        // Markings
        ko.components.register(config.markingsComponent, {
            viewModel: { require: 'app/components/markings/markings' },
            template: { require: 'text!app/components/markings/markingsView.html' }
        });

        // Questions
        ko.components.register(config.questionsComponent, {
            viewModel: { require: 'app/components/questions/questions' },
            template: { require: 'text!app/components/questions/questionsView.html' }
        });

        // Question
        ko.components.register(config.questionComponent, {
            viewModel: { require: 'app/components/question/question' },
            template: { require: 'text!app/components/question/questionView.html' }
        });

        // Comments
        ko.components.register(config.commentsComponent, {
            viewModel: { require: 'app/components/comments/comments' },
            template: { require: 'text!app/components/comments/commentsView.html' }
        });

        // Answers
        ko.components.register(config.answersComponent, {
            viewModel: { require: 'app/components/answers/answers' },
            template: { require: 'text!app/components/answers/answersView.html' }
        });

        // homepage page
        ko.components.register(config.homeComponent, {
            viewModel: { require: 'app/components/homepage/homepage' },
            template: { require: 'text!app/components/homepage/homepageView.html' }
        });
        // Search page
        ko.components.register(config.searchpagesComponent, {
            viewModel: { require: 'app/components/searchpage/searchpage' },
            template: { require: 'text!app/components/searchpage/searchpageView.html' }
        });
       // Customization page
        ko.components.register(config.customizationComponent, {
            viewModel: { require: 'app/components/customization/customization' },
            template: { require: 'text!app/components/customization/customizationView.html' }
        });
        // Users page
        ko.components.register(config.usersComponent, {
            viewModel: { require: 'app/components/users/users' },
            template: { require: 'text!app/components/users/usersView.html' }
        });

        // JCloud page
        ko.components.register(config.jcloudComponent, {
            viewModel: { require: 'app/components/jcloud/jcloud' },
            template: { require: 'text!app/components/jcloud/jcloudView.html' }
        });
        // termnetwork page
        ko.components.register(config.concurrentsComponent, {
            viewModel: { require: 'app/components/concurrents/concurrents' },
            template: { require: 'text!app/components/concurrents/concurrentsView.html' }
        });
        // annotations page
        ko.components.register(config.annotationsComponent, {
            viewModel: { require: 'app/components/annotations/annotations' },
            template: { require: 'text!app/components/annotations/annotationsView.html' }
        });
        
        
        ko.applyBindings(viewmodel);
    });