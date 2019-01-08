using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.DTO;
using WebService.Models;
using DataService.DataAccessLayer;
using AutoMapper;
using Microsoft.AspNetCore.Routing;


namespace WebService.Controllers
{
    [Route("api")]
    public class QuestionController : CustomeController
    { 
        private readonly IRepository _repository;
    private readonly IMapper _mapper;


    public QuestionController(IRepository _repository, IMapper mapper)
    {
        this._repository = _repository;
        this._mapper = mapper;

    }
        //Get All Questions
        [HttpGet("question", Name = nameof(GetQuestions))]
        public IActionResult GetQuestions(int page = 0, int pageSize = 12)
        {
            CheckPageSize(ref pageSize);

            var total = _repository.CountQuestions();
            var totalPages = GetTotalPages(pageSize, total);

            var data = _repository.GetQuestions(page, pageSize)
                .Select(x => new QuestionModel
                {
                    PostId = x.Id,
                    Url = Url.Link(nameof(QuestionController.GetQuestionById), new { Qid = x.Id }),
                    UserName = x.UserInfo.OwnerUserDisplayName,
                    CreationDate = x.CreationDate,
                    Score = x.Score,
                    Title = x.Title,
                    // We comment some of the following since delivery of these data in this part is not necessary
                  //  Body = x.Body,
                    Tags = _repository.GetPostTagsByPostId(x.Id).Select(t =>  t.Tag.Tag ).ToList(),
                   /* LinkedPosts = _repository.GetLinkedPosts(x.Id).Select(l => new LinkedPostsModel
                    {
                        LinkedPostUrl = Url.Link(nameof(GetQuestionById), new { Qid = l.Id }),
                        PostTitle = l.Title
                    }).ToList(), */
                    UserUrl = Url.Link(nameof(UserController.GetUserByUserId), new { Uid = x.OwnerUserId }),
                 //   AcceptedAnswerUrl = Url.Link(nameof(AnswerController.GetAnswerById), new { Qid = x.Id, Aid = x.AcceptedAnswerId }),
                    //AnswersUrl = Url.Link(nameof(AnswerController.GetAnswersByQuestionId), new { Qid = x.Id }),
                   // CommentsUrl = Url.Link(nameof(CommentController.GetCommentsByQuestionId), new { Qid = x.Id }),
                    MarkThisPost = _repository.GetMarkingById(x.Id)== null?
                    Url.Link(nameof(MarkingController.AddMarking),new {Pid= x.Id }):
                    "Already marked",
                    UnMarkPost = _repository.GetMarkingById(x.Id) == null ?
                    "Not marked yet" 
                    : Url.Link(nameof(MarkingController.RemoveMarking), new { Pid = x.Id }),

                });

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetQuestions), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetQuestions), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetQuestions), page, pageSize),
                Data = data
            };

            return Ok(result);
        }


        // Get Question by Id

        [HttpGet("question/{Qid}", Name = nameof(GetQuestionById))]
    public IActionResult GetQuestionById(int Qid)
    {

        var Question = _repository.GetQuestionById(Qid);
        if (Question == null) return NotFound();

        var model = _mapper.Map<QuestionModel>(Question);
        model.UserName = Question.UserInfo.DisplayName;
        model.Tags = _repository.GetPostTagsByPostId(Qid).Select(t => t.Tag.Tag).ToList();
            model.LinkedPosts = Question.LinkedPosts.Select(l => new LinkedPostsModel {
                Id = l.Id,
                LinkedPostUrl = Url.Link(nameof(GetQuestionById), new { Qid = l.Id }),
                PostTitle = l.Title
        }).ToList();
        model.PostId = Question.Id;
        model.Url = Url.Link(nameof(GetQuestionById), new { Qid = Question.Id });
        model.UserUrl = Url.Link(nameof(UserController.GetUserByUserId), new { Uid = Question.OwneruserId });
        model.AcceptedAnswerUrl = Url.Link(nameof(AnswerController.GetAnswerById), new { id = Question.AcceptedAnswerId });
        model.AnswersUrl = Url.Link(nameof(AnswerController.GetAnswersByQuestionId), new { Qid = Question.Id });
        model.CommentsUrl = Url.Link(nameof(CommentController.GetCommentsByQuestionId), new { Qid = Question.Id });
        model.MarkThisPost = _repository.GetMarkingById(Question.Id) == null ?
              Url.Link(nameof(MarkingController.AddMarking), new { Pid = Question.Id }) :
              "Already marked";
        model.UnMarkPost = _repository.GetMarkingById(Question.Id) == null ?
              "Not marked yet"
              : Url.Link(nameof(MarkingController.RemoveMarking), new { Pid = Question.Id });


        return Ok(model);
    }

        // Questions of A user
        [HttpGet("users/{Uid}/question", Name = nameof(GetQuestionsByUserId))]
        public IActionResult GetQuestionsByUserId(int Uid, int page = 0, int pageSize = 5)
        {
            CheckPageSize(ref pageSize);

            var total = _repository.CountQuestionsByUserId(Uid);
            var totalPages = GetTotalPages(pageSize, total);

            var data = _repository.GetQuestionsByUserID(Uid, page, pageSize)
                .Select(x => new QuestionModel
                {
                    PostId = x.Id,
                    Url = Url.Link(nameof(QuestionController.GetQuestionById), new { Qid = x.Id }),
                    UserName = x.UserInfo.DisplayName,
                    CreationDate = x.CreationDate,
                    Score = x.Score,
                    Title = x.Title,
                    Body = x.Body,
                    Tags = _repository.GetPostTagsByPostId(x.Id).Select(t => t.Tag.Tag).ToList(),
                    LinkedPosts = x.LinkedPosts.Select(l => new LinkedPostsModel
                    {
                        LinkedPostUrl = Url.Link(nameof(GetQuestionById), new { Qid = l.Id }),
                        PostTitle = l.Title
                    }).ToList(),
            UserUrl = Url.Link(nameof(UserController.GetUserByUserId), new { Uid = x.OwneruserId }),
                    AcceptedAnswerUrl = Url.Link(nameof(AnswerController.GetAnswerById), new { id = x.AcceptedAnswerId }),
                    AnswersUrl = Url.Link(nameof(AnswerController.GetAnswersByQuestionId), new { Qid = x.Id }),
                    CommentsUrl = Url.Link(nameof(CommentController.GetCommentsByQuestionId), new { Qid = x.Id }),
                    MarkThisPost = _repository.GetMarkingById(x.Id) == null ?
                      Url.Link(nameof(MarkingController.AddMarking), new { Pid = x.Id}) :
                      "Already marked",

        });

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetQuestions), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetQuestions), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetQuestions), page, pageSize),
                Data = data
            };

            return Ok(result);
        }



    }
}
