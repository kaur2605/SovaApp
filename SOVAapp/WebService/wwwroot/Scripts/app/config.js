define([], function () {
    var server = 'http://localhost:5001';

    var api = "/api/";

    var applicationName = "SOVA";

    var menuElements = [
        "Home",                            // 0
        "Search",                          // 1
        "Questions",                       // 2
        "Users",                           // 3
        "Markings",                        // 4
        "Customization",                   // 5
        "Concurrents"                      // 6
    ];

    var nonMenuComponentElements = [
        "Question",                         // 0 
        "Comments",                         // 1
        "Answers",                          // 2
        "Annotations"                       // 3
    ];


    return {
        // back-end routes
        HomeUrl: server + api,
        questionsUrl: server + api + "question",
        markingsUrl: server + api + "marking/",
        searchUrl: server + api + "search/",
        usersUrl: server + api + "user",
        customizationUrl: server + api + menuElements[5].toLowerCase(),
        searchHistoryUrl: server + api + "searchhistory",
        concurrentsUrl: server + api + "termnetwork/",
        
        // menu
        menuElements: menuElements,
        defaultMenuItem: menuElements[0].toLowerCase(),       

        // components
        menuComponent: "topbarmenu",
        homeComponent: menuElements[0].toLowerCase(),
        searchpagesComponent: menuElements[1].toLowerCase(),
        questionsComponent: menuElements[2].toLowerCase(),
        usersComponent: menuElements[3].toLowerCase(),
        markingsComponent: menuElements[4].toLowerCase(),
        customizationComponent: menuElements[5].toLowerCase(),
        concurrentsComponent: menuElements[6].toLowerCase(),

        questionComponent: nonMenuComponentElements[0].toLowerCase(),
        commentsComponent: nonMenuComponentElements[1].toLowerCase(),
        answersComponent: nonMenuComponentElements[2].toLowerCase(),
        annotationsComponent: nonMenuComponentElements[3].toLowerCase(),

        jcloudComponent: "jcloud",
        applicationName: applicationName
       
      
    }
});