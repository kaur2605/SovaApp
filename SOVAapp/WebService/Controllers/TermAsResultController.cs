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
    [Route("api/TermAsResult")]
    public class TermAsResultController : CustomeController
    {


        private readonly IRepository _repository;
        private readonly IMapper _mapper;


        public TermAsResultController(IRepository _repository, IMapper mapper)
        {
            this._repository = _repository;
            this._mapper = mapper;

        }


        [HttpGet("{Pid}", Name = nameof(GetTermByPost))]

        public IActionResult GetTermByPost(int Pid)
        {
            


            var Terms = _repository.GetTermsByPostId(Pid);
            if (Terms.Count == 0)
            {
                return NotFound("No results have been found");
            }
            var data = Terms.Select(x => new TermAsResultModel
            {
                text = x.Word,
                weight = x.Score


            });
          
         
            return Ok(data);
        }



/*
        [HttpGet("{SearchText}", Name = nameof(SearchTermAsResult))]

        public IActionResult SearchTermAsResult(string SearchText, int page = 0, int pageSize = 5)
        {
            CheckPageSize(ref pageSize);


            var Terms = _repository.GetTermsAsResult(SearchText, page, pageSize);
            if (Terms.Count == 0)
            {
                return NotFound("No results have been found");
            }
            var data = Terms.Select(x => new TermAsResultModel
            {
                word = x.Word,
                tf = x.Score


            });
            var total = _repository.CountTermsAsResult(SearchText);
            var totalPages = GetTotalPages(pageSize, (int)total);

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(SearchTermAsResult), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(SearchTermAsResult), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(SearchTermAsResult), page, pageSize),
                Data = data
            };

            return Ok(result);
        }
        */


    }
}
