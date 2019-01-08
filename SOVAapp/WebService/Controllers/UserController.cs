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

    [Route("api/user")]

    public class UserController: CustomeController
    {  
    
         private readonly IRepository _repository;
         private readonly IMapper _mapper;
     

        public UserController(IRepository _repository, IMapper mapper)
        {
        this._repository = _repository;
           this._mapper = mapper;
         
        }


        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers(int page = 0, int pageSize = 8)
        {
            CheckPageSize(ref pageSize);

            var total = _repository.CountUsers();
            var totalPages = GetTotalPages(pageSize, total);

            var data = _repository.GetUsers(page, pageSize)
                .Select(x => new UserInfoModel
                {
                    Url = Url.Link(nameof(GetUserByUserId), new { Uid = x.OwnerUserId }),
                    DisplayName = x.OwnerUserDisplayName,
                    CreateDate = x.CreationDate,
                    Location = x.OwnerUserLocation,
                    QuestionsUrl =
                    (_repository.UserHasQuestion(x.OwnerUserId)) ?
                    Url.Link(nameof(QuestionController.GetQuestionsByUserId), new { Uid = x.OwnerUserId }) : null,
                    AnswersUrl = Url.Link(nameof(AnswerController.GetAnswersByUserId), new { Uid = x.OwnerUserId }),
                    CommentsUrl = Url.Link(nameof(CommentController.GetCommentsByUserId), new { Uid = x.OwnerUserId }),
                });

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetUsers), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetUsers), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetUsers), page, pageSize),
                Data = data
            };

            return Ok(result);
        }



        [HttpGet("{Uid}",Name = nameof(GetUserByUserId))]
        public IActionResult GetUserByUserId( int Uid)
    {
            
            var User = _repository.GetUserById(Uid);
            if (User == null) return NotFound();
           
            var model = _mapper.Map<UserInfoModel>(User);
            model.Url = Url.Link(nameof(GetUserByUserId), new { Uid = User.Id });
            model.QuestionsUrl = Url.Link(nameof(QuestionController.GetQuestionsByUserId), new { Uid = User.Id });
            model.AnswersUrl = Url.Link(nameof(AnswerController.GetAnswersByUserId), new { Uid = User.Id });
            model.CommentsUrl = Url.Link(nameof(CommentController.GetCommentsByUserId), new { Uid = User.Id });
            return Ok(model);
        }


    

    }
}
