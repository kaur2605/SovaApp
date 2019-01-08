using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;
using DataService.DataAccessLayer;
using AutoMapper;
using Microsoft.AspNetCore.Routing;

namespace WebService.Controllers
{
    [Route("api")]
    public class HomeController : CustomeController
    {
        private readonly IRepository _repository;
        private IMapper _Imapper;

        public HomeController(IRepository _Irepository, IMapper _Imapper)
        {

            this._repository = _Irepository;
            this._Imapper = _Imapper;

        }

        [HttpGet(Name = nameof(GetHomeContents))]
        public IActionResult GetHomeContents()
        {
            HomeModel model = new HomeModel()
            {

                QuestionsUrl = Url.Link(nameof(QuestionController.GetQuestions), new { }),
                UsersUrl = Url.Link(nameof(UserController.GetUsers), new { }),
                RecommendedQuestions = _repository.ShowCustomePosts().Select(i => new CustomPostModel
                {
                    PostId = i.PostId,
                    Title = i.Title,
                    // Body = i.Body,
                    QuestionUrl = Url.Link(nameof(QuestionController.GetQuestionById), new { Qid = i.PostId }),
                    User = _repository.GetUserById(i.UserId).DisplayName,
                    Tags = _repository.GetPostTagsByPostId(i.PostId).Select(t => t.Tag.Tag).ToList(),
                    Score = _repository.GetScoreByPostId(i.PostId)



                }).ToList(),
                MarkingsUrl = Url.Link(nameof(MarkingController.GetMarkings), new { }),
                Search = Url.Link(nameof(SearchController.DoSearch), new { SearchText = "Search_Text_Here" }),
                TermsByPostId = Url.Link(nameof(TermAsResultController.GetTermByPost), new { Pid = 19 }),
                TermNetwrok = Url.Link(nameof(Co_OccurrenceController.GetCoOccurrencesByWord), new { word = "Term_Here" }),
                SearchHistoryUrl = Url.Link(nameof(SearchHistoryController.GetSearchHistories), new { }),
                CustomeFieldUrl = Url.Link(nameof(UserCustomeFieldController.GetUserCustomeField), new { })

            };
        
      
            
            
            return Ok(model);
        }
    


}
}
