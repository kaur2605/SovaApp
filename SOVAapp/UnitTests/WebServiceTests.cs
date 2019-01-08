using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using System.Diagnostics;
using DataService.DataAccessLayer;
using Moq;
using AutoMapper;
using WebService.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests
{
    public class WebServiceTest: Helpers
    {
      
        [Fact]
        public void ApiGetAllQuestions_ReturnListOfQuestions_withValues()
        {
            string QuestionsApi = "http://localhost:5001/api/question";
            var (data, statusCode) = GetObject(QuestionsApi);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(2237, data["total"]);
            Assert.Equal("Chris Jester-Young", data["data"][0]["userName"]);
            Assert.Equal(4, data["data"][1]["tags"].Count());

        }

        [Fact]
        public void ApiQuestions_GetWithId_ContainsURL()
        {       
            string QuestionsApi = "http://localhost:5001/api/question/19";

            var(data, statusCode) = GetObject(QuestionsApi);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("http://localhost:5001/api/question/19", data["url"]);
            Assert.Equal(14, data.Count);

        }

        [Fact]
        public void ApiUser_InvalidId_NotFound()
        {
            string UserApi = "http://localhost:5001/api/user";
            var (user, statusCode) = GetObject($"{UserApi}/0");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void ApiCustomeField_AddNewCustomeField()
        {

            var client = new HttpClient();
            var response = client.PostAsync("http://localhost:5001/api/customization/5_sql,wordpress,jumla", null).Result;
            Assert.Equal(true , response.IsSuccessStatusCode);
            string CustomeApi = "http://localhost:5001/api/customization";
            var (data, statusCode) = GetObject(CustomeApi);
            Assert.Equal("jumla", data["favortieTags"][2]);
            //Checking that the postlimit of Recommended questions according to our favorite tags equals 5
            string HomeApi = "http://localhost:5001/api";
            var (data2, statusCode2) = GetObject(HomeApi);
            Assert.Equal(5, data2["recommendedQuestions"].Count());

        }


 [Fact]
        public void ApiGetAllMarking_ReturnListOfMarking_withValues()

        {

            var client = new HttpClient();
            var response = client.PostAsync("http://localhost:5001/api/marking/1667278", null).Result;
            string MarkingApi = "http://localhost:5001/api/Marking";
            var (data, statusCode) = GetObject(MarkingApi);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(data["data"].Count(),5);
            int totalPages = (int)data["pages"];
            var newUrl = "http://localhost:5001/api/marking?page="+(totalPages-1)+"&pageSize=5";
            var (data2, statusCode2) = GetObject(newUrl);
            Assert.Equal("http://localhost:5001/api/marking/1667278", data2["data"].Last["markingUrl"]);
            response = client.DeleteAsync("http://localhost:5001/api/marking/1667278").Result;

        }

        public void ApiGetsearchHistory_ReturnListOfSearchHistory_withValues()
        {
            string SearchHistoryApi = "http://localhost:5001/api/searchhistory";
            var (data, statusCode) = GetObject(SearchHistoryApi);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(337, data["total"]);
            Assert.Equal("angular", data["data"][1]["searchText"]);

        }


        public void ApiGetUser_ReturnListOfUsers()
        {
            string UserApi = "http://localhost:5001/api/user";
            var (data, statusCode) = GetObject(UserApi);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(11392, data["total"]);
            Assert.Equal("Jeff Atwood", data["data"][0]["displayName"]);
            String UserId = "http://localhost:5001/api/user/3";
            var (data2, StatusCode) = GetObject(UserId);
            Assert.Equal("New York, NY", data2["data"][1]["location"]);


        }

        [Fact]
        public void GetQuestionById_InvalidId_ReturnsNotFund()
        {
            var dataServviceMock = new Mock<IRepository>();
            var imapperMock = new Mock<IMapper>();

            var ctrl = new QuestionController(dataServviceMock.Object, imapperMock.Object);

            var response = ctrl.GetQuestionById(1);
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void ApiGetQuestionCommentById_ReturnListofComments_withValues()
        {
            string CommentApi = "http://localhost:5001/api/comment";
            var (data, statusCode) = GetObject(CommentApi);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
            var CommentId = "http://localhost:5001/api/question/652788/answer/652802/comment/466139";
            var (data2, statusCode2) = GetObject(CommentId);
            Assert.Equal("Joel Spolsky", data2["userName"]);
            Assert.Equal("only if you're a valley girl", data2["body"]);
        }

        [Fact]
        public void ApiGetUserInfobyAnswerId_Returninfo_withValues()
        {
            string AnswerIdApi = "http://localhost:5001/api/question/5323/answer/5345";
            var (data, statusCode) = GetObject(AnswerIdApi);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Jon Galloway", data["userName"]);
            Assert.Equal(8, data["score"]);
            Assert.Equal("http://localhost:5001/api/question/5323/answer/5345/comment", data["commentsUrl"]);
            Assert.Equal(8, data.Count);
        }

        [Fact]
        public void ApiGetAnswersbyUserId_ReturnAnswer_withValues()
        {
            string AnswerwithUserIdApi = "http://localhost:5001/api/user/3/answer";
            var (data, statusCode) = GetObject(AnswerwithUserIdApi);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Jarrod Dixon", data["data"][0]["userName"]);
            Assert.Equal("http://localhost:5001/api/user/3", data["data"][0]["userUrl"]);
            Assert.Equal(19, data["data"][0]["score"]);
        }

       [Fact]
        public void ApiAnnotations_InvalidId_NotFound()
        {
            string AnnotationsApi = "http://localhost:5001/api/marking/19/annotation/NewAnnotation_0_0";
            var (Annotations, statusCode) = GetObject($"{AnnotationsApi }/0");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }


        // IR tests

        [Fact]
        public void Search_MustReturn_Valid_Results()
        {
            //Search text = what is functional programming
            string Searching = "http://localhost:5001/api/search/what%20is%20functional%20programming";
            var (data, statusCode) = GetObject(Searching);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            var totalResults = (int)data["total"];
            Assert.Equal(15, totalResults);
            string postTitle = (string)data["data"][1]["postTitle"];
            Assert.Equal("Pure Functional Language: Haskell", postTitle);     
        }

        [Fact]
        public void Search_MustReturn_Valid_Url()
        {
            //Search text = What is parallel programming
            string Searching = "http://localhost:5001/api/search/what%20is%20parallel%20programming";
            var (data, statusCode) = GetObject(Searching);
            string postUrl =  (string)data["data"][0]["postUrl"];
            Assert.Equal("http://localhost:5001/api/question/194812", postUrl);
            var (data2, statusCode2) = GetObject(postUrl);
            var user = data2["userName"];
            Assert.Equal("Karan", user);
            var creationDate = data2["creationDate"];
            Assert.Equal(creationDate, "2008-10-11T23:17:54");
        }



        [Fact]
        public void CoOccurrenceUrl_must_return_valid_values()
        {
            string url = "http://localhost:5001/api/cooccurrence/java";
            var (data, statusCode) = GetObject(url);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal( 232, (int)data["total"]);
            Assert.Equal("Java", (string)data["data"][2]["word1"]);
            Assert.Equal("String", (string)data["data"][0]["word2"]);
        }


        [Fact]
        public void TermAsResult_must_return_valid_values()
        {
            //Search text = How to change background color
             string searching = "http://localhost:5001/api/termasresult/how%20to%20change%20background%20color";
             var (data, statusCode) = GetObject(searching);
             Assert.Equal(HttpStatusCode.OK, statusCode);
             Assert.Equal(15, (int)data["total"]);
             Assert.Equal("attribute", (string)data["data"][0]["word"]);
        }




    }
}
