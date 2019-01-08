define(['jquery', 'app/config'], function ($, conf) {
    return {


        getAnswers: function (url, callback) {
            if (url === undefined) {
                return;
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },
        getCustomefield: function (url, callback) {
            if (callback == undefined) {
                callback = url;
              url = conf.customizationUrl;
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },
        getComments: function (url, callback) {
            if (url === undefined) {
                return;
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },
        getData: function (url, callback) {
            if (url == undefined) {
                return;
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },

        getQuestion: function (url, callback) {
            $.getJSON(url, function (data) {
                callback(data);
            });
        },
        getTermsByPost: function (url, callback) {
            $.getJSON(url, function (data) {
                callback(data);
            });
        },
        getTermsCode: function (url, callback) {
            $.getJSON(url, function (data) {
                callback(data);
            });
        },

        getQuestions: function (url, callback) {

             if (url == undefined) {
                url = conf.questionsUrl;
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },

        getMarkings: function (url, callback) {
            if (callback == undefined) {
                callback = url;
                url = "http://localhost:5001/api/marking";
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },

        getSearchResult: function (url, callback) {

            $.getJSON(url, function (data) {
                callback(data);
            });
        },

        getAnnotations: function (url, callback) {

            $.getJSON(url, function (data) {
                callback(data);
            });
        },
        getSearchHistory: function (url, callback) {

            if (callback == undefined) {
                callback = url;
                url = conf.searchHistoryUrl;
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },

        getUsers: function (url, callback) {
            if (callback == undefined) {
                callback = url;
                url = conf.usersUrl;
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },
        getFavoriteQuestions: function (url, callback) {
            if (callback == undefined) {
                callback = url;
                url = conf.HomeUrl;
            }
            $.getJSON(url, function (data) {
                callback(data);
            });
        },
        updateData: function (url, data, callback) {
            $.ajax({
                type: 'PUT',
                url: url,
                data: data,
                success: callback

            });
        },
        deleteData: function (url, data, callback) {
            $.ajax({
                type: 'Delete',
                url: url,
                data: data,
                success: callback

            });
        },
     

        postData: function (url, data, callback) {
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                success: callback

            });
        }
        }
    
});