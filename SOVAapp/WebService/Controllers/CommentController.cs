using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.DataAccessLayer;
using DataService.DTO;
using System.Web.Http.Routing;
using WebService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Routing;



namespace WebService.Controllers
{
    [Route("api")]
    public class CommentController : CustomeController
    { 
     private readonly IRepository _repository;
    private readonly IMapper _mapper;


    public CommentController(IRepository _repository, IMapper mapper)
    {
        this._repository = _repository;
        this._mapper = mapper;

    }

        //Question Comments
        [HttpGet("question/{Qid}/comment", Name = nameof(GetCommentsByQuestionId))]
        public IActionResult GetCommentsByQuestionId(int Qid, int page = 0, int pageSize = 500)
        {
            CheckPageSize(ref pageSize);

            var total = _repository.CountCommentsByPostId(Qid);
            
            var totalPages = GetTotalPages(pageSize, total);

            var data = _repository.GetCommentsByPostId(Qid, page, pageSize)
                .Select(x => new CommentModel
                {
                 Url = Url.Link(nameof(GetQuestionCommentById), new { commentId = x.CommentId }),
                 UserName = x.UserInfo.DisplayName,
                 CreationDate = x.CreationDate,
                 Score = x.Score,
                 Body = x.Body,
                 PostUrl = Url.Link(nameof(QuestionController.GetQuestionById), new { id = x.post.Id }),
                 UserUrl = Url.Link(nameof(UserController.GetUserByUserId), new { Uid = x.User }),
                  
                });

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetCommentsByQuestionId), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetCommentsByQuestionId), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetCommentsByQuestionId), page, pageSize),
                Data = data
            };

            return Ok(result);
        }


        //Question's each Comment
        [HttpGet("question/{Qid}/comment/{commentId}", Name = nameof(GetQuestionCommentById))]

        public IActionResult GetQuestionCommentById(int commentId)
        {

            var Comment = _repository.GetCommentById(commentId);

            if (Comment == null) return NotFound();

  
            var model = new CommentModel();

            model.Url = Url.Link(nameof(GetQuestionCommentById), new { commentId = Comment.CommentId });
            model.UserName = Comment.UserInfo.DisplayName;
            model.Score = Comment.Score;
            model.Body = Comment.Body;
            model.CreationDate = Comment.CreationDate;
            model.UserUrl = Url.Link(nameof(UserController.GetUserByUserId), new { Uid = Comment.User });
            model.PostUrl = Url.Link(nameof(QuestionController.GetQuestionById), new { id = Comment.post.Id });

            return Ok(model);
        }


        //Answers Comments
        [HttpGet("question/{Qid}/answer/{Aid}/comment", Name = nameof(GetCommentsByAnswerId))]
    
        public IActionResult GetCommentsByAnswerId(int Aid, int page = 0, int pageSize = 500)
        {
            CheckPageSize(ref pageSize);
            
            var total = _repository.CountCommentsByPostId(Aid);
            var totalPages = GetTotalPages(pageSize, total);
          
            var data = _repository.GetCommentsByPostId(Aid, page, pageSize)
                .Select(x => new CommentModel
                {
                    Url = Url.Link(nameof(CommentController.GetCommentById), new { Qid = _repository.GetPostById(Aid).ParentId, Aid = Aid, commentId = x.CommentId }),
                    UserName = x.UserInfo.DisplayName,
                    CreationDate = x.CreationDate,
                    Score = x.Score,
                    Body = x.Body,
                    PostUrl = Url.Link(nameof(AnswerController.GetAnswerById), new { id = x.post.Id }),
                    UserUrl = Url.Link(nameof(UserController.GetUserByUserId), new { Uid = x.User }),

                });
            
            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetCommentsByAnswerId), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetCommentsByAnswerId), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetCommentsByAnswerId), page, pageSize),
                Data = data
            };

            return Ok(result);
        }
        //Answers each Comment
        [HttpGet("question/{Qid}/answer/{Aid}/comment/{commentId}", Name = nameof(GetCommentById))]

    public IActionResult GetCommentById(int commentId)
    {
          
        var Comment = _repository.GetCommentById(commentId);
           
        if (Comment == null) return NotFound();

          
            var model = new CommentModel();
    
            model.Url = Url.Link(nameof(GetCommentById), new { id = Comment.CommentId });
            model.UserName = Comment.UserInfo.DisplayName;
            model.Score = Comment.Score;
            model.Body = Comment.Body;
            model.CreationDate = Comment.CreationDate;
            model.UserUrl = Url.Link(nameof(UserController.GetUserByUserId), new { Uid = Comment.User });
            model.PostUrl = Url.Link(nameof(QuestionController.GetQuestionById), new { id = Comment.post.Id });

            return Ok(model);
    }

        //User's Comments
        [HttpGet("user/{Uid}/comment", Name = nameof(GetCommentsByUserId))]

        public IActionResult GetCommentsByUserId(int Uid, int page = 0, int pageSize = 2)
        {
            CheckPageSize(ref pageSize);

            var total = _repository.CountCommentsByUserId(Uid);
            var totalPages = GetTotalPages(pageSize, total);

            var data = _repository.GetCommentsByUserId(Uid, page, pageSize)
                .Select(x => new CommentModel
                {
                    Url = Url.Link(nameof(GetCommentById), new { Qid = _repository.GetPostById(x.PostId).ParentId, Aid = x.PostId, commentId = x.CommentId }),
                    UserName = x.UserInfo.DisplayName,
                    CreationDate = x.CreationDate,
                    Score = x.Score,
                    Body = x.Body,
                    // Checking whether the post is an answer or question to give it the correct link
                    PostUrl = _repository.GetPostById(x.PostId).PostTypeId == 2?
                     Url.Link(nameof(AnswerController.GetAnswerById), new {Qid = _repository.GetPostById(x.PostId).ParentId, Aid = x.PostId }):
                     Url.Link(nameof(QuestionController.GetQuestionById), new { Qid = x.PostId }),
                    UserUrl = Url.Link(nameof(UserController.GetUserByUserId), new { Uid = x.User }),

                });

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetCommentsByAnswerId), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetCommentsByAnswerId), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetCommentsByAnswerId), page, pageSize),
                Data = data
            };

            return Ok(result);
        }





    }
}
