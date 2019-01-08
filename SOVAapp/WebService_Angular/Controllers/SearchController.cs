using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.DataAccessLayer;
using DataService.DTO;
using WebService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Routing;



namespace WebService.Controllers
{
    [Route("api/search")]
    public class SearchController : CustomeController
    {


        private readonly IRepository _repository;
        private readonly IMapper _mapper;


        public SearchController(IRepository _repository, IMapper mapper)
        {
            this._repository = _repository;
            this._mapper = mapper;

        }


        [HttpGet("{SearchText}", Name = nameof(DoSearch))]

        public IActionResult DoSearch(string SearchText,int page = 0, int pageSize = 7)
        {
            CheckPageSize(ref pageSize);


            var searches = _repository.Search(SearchText, page, pageSize);
            if (searches.Count == 0)
            {
                return NotFound("No results have been found");
            }
            var data = searches.Select(x => new SearchModel
            {
                SearchText = SearchText,
                PostTitle = x.Title,
                PostUrl = Url.Link(nameof(QuestionController.GetQuestionById), new { Qid = x.Id }),
                PostId = x.Id,
                //  PostBody = x.Body
                jCloudUrl = Url.Link(nameof(TermAsResultController.GetTermByPost), new {Pid = x.Id }),
            
                Tags = x.Tags.Select(a => a.Tag.Tag).ToList()
               

            });
            var total = searches.First().totalResults;
            var totalPages = GetTotalPages(pageSize, (int)total);

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(DoSearch), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(DoSearch), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(DoSearch), page, pageSize),
                Data = data
            };

            return Ok(result);
        }

    }
    }

