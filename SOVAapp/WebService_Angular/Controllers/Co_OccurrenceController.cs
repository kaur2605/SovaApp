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
    [Route("api")]
    public class Co_OccurrenceController : CustomeController
    {

        private readonly IRepository _repository;

        public Co_OccurrenceController(IRepository _repository)
        {
            this._repository = _repository;

        }

        [HttpGet("cooccurrence", Name = nameof(GetCoOccurrences))]

        public IActionResult GetCoOccurrences(int page = 0, int pageSize = 5)
        {
            CheckPageSize(ref pageSize);

            var total = _repository.CountCoOccurrences();
            var totalPages = GetTotalPages(pageSize, total);

            var data = _repository.GetCoOccurrences(page, pageSize)
                .Select(x => new Co_OccurrendeModel
                {
                    Word1 = x.Word,
                    Word2 = x.Word2,
                    Grade = x.Grade     

                });

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetCoOccurrences), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetCoOccurrences), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetCoOccurrences), page, pageSize),
                Data = data
            };

            return Ok(result);
        }


        [HttpGet("cooccurrence/{word}", Name = nameof(GetCoOccurrencesByWord))]

        public IActionResult GetCoOccurrencesByWord(string word, int page = 0, int pageSize = 5)
        {
            CheckPageSize(ref pageSize);

            var total = _repository.CountCoOccurrencesByWord(word);
            var totalPages = GetTotalPages(pageSize, total);

            var data = _repository.GetCoOccurrencesByWord(word , page , pageSize)
                .Select(x => new Co_OccurrendeModel
                {
                    Word1 = x.Word,
                    Word2 = x.Word2,
                    Grade = x.Grade

                });

            var result = new
            {
                Total = total,
                Pages = totalPages,
                Page = page,
                Prev = Link(nameof(GetCoOccurrencesByWord), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetCoOccurrencesByWord), page, pageSize, 1, () => page < totalPages - 1),
                Url = Link(nameof(GetCoOccurrencesByWord), page, pageSize),
                Data = data
            };

            return Ok(result);
        }


    }
}
