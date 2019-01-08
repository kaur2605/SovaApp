using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataService.DataAccessLayer;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly IRepository _repository;

        public SampleDataController(IRepository _repository)
        {
            this._repository = _repository;
        }

        [HttpGet("[action]")]
        public IActionResult GetQuestions(int page = 0, int pageSize = 12)
        {
         

         

            var data = _repository.GetQuestions(page, pageSize)
                .Select(x => new QuestionModel
                {
                    PostId = x.Id,
                    UserName = x.UserInfo.OwnerUserDisplayName,
                    Score = x.Score,
                    Title = x.Title,

                 
                });

        

            return Ok(data);
        }


        public class QuestionModel
        {
            public int PostId { get; set; }
            public string UserName { get; set; }
            public int Score { get; set; }
            public string Title { get; set; }
           

        }
    }
}
