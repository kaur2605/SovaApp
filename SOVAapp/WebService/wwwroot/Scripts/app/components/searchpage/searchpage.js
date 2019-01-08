define(['knockout', 'app/dataservice', 'app/config','helpers'], function (ko, dataservice, config) {
    return function () {
        var searchData = ko.observableArray();
        var searchText = ko.observable();
        var searchprev = ko.observable();
        var searchnext = ko.observable();
        var searchtotal = ko.observable();
        var searchpage = ko.observable('null');
        var pagenumber = ko.computed(function () {
            if (searchpage()!=='null'){
                return searchpage() + 1;
            } return '';
        });
        var totalPages = ko.observable();

        var searchhistory = ko.observableArray();
        var jcloudComponent = ko.observable(config.jcloudComponent);  
        var isSearching = ko.observable(false);
        var noElements = ko.computed(function () {
            if (isSearching()){
                return searchData().length === 0;
            } 
            return false;
        });

        var updateTheLastHistory = ko.computed(function () {
            if (searchData().length!==0)
         
                dataservice.getSearchHistory(function (data) {
                    searchhistory(data.data);
                });
            
            return "updated";

        });
        var callback = function (data) {
            searchData(data.data);
            searchpage(data.page);
            searchprev(data.prev);
            searchnext(data.next);
            searchtotal(data.total);
            totalPages(data.pages);
        }
        dataservice.getSearchHistory(function (data) {
            searchhistory(data.data);
        });
     

        var startSearching = function () {
            searchData([]);
            isSearching(true); 
            var url = config.searchUrl + ' ' + searchText();
            console.log(url);
            dataservice.getSearchResult(url, callback);
        }

        var searchItAgain = function (search) {
            searchData([]);
            isSearching(true);
            var url = config.searchUrl + ' ' +  search;
            searchText(search);
            dataservice.getSearchResult(url, callback);
             
        }
        var prevClick = function () {
            searchData([]);
            dataservice.getSearchResult(searchprev(), callback);
            console.log(searchprev());
        };
        var nextClick = function () {
            searchData([]);
            dataservice.getSearchResult(searchnext(), callback);
            console.log(searchnext());
        };
         
        var gotoquestion = function (questionUrl, root) {
            postman.notify({ component: config.questionComponent, url: questionUrl, prevComponent: root.currentComponent() }, "currentComponent");
        };
      
  
        return {
            prevClick: prevClick,
            nextClick: nextClick,
            searchData:searchData,
            searchText: searchText,
            startSearching: startSearching,
            pageNumber: pagenumber,
            prev: searchprev,
            next: searchnext,
            total: searchtotal,
            gotoquestion: gotoquestion,
            searchhistory: searchhistory,
            searchItAgain: searchItAgain,
            jcloudComponent: jcloudComponent,
            noElements,
            totalPages
            
        }
    }
});