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
    [Route("api/termnetwork")]
    public class TermNetworkCodeController : CustomeController
    {


        private readonly IRepository _repository;
        private readonly IMapper _mapper;


        public TermNetworkCodeController(IRepository _repository, IMapper mapper)
        {
            this._repository = _repository;
            this._mapper = mapper;

        }


        [HttpGet("{Word}", Name = nameof(GenerateTermNetworkCode))]

        public IActionResult GenerateTermNetworkCode(string Word)
        {
            


            var Code = _repository.GenerateTermNetworkCode(Word);

            if (Code == null)
            {
                return NotFound("No results have been found");
            }
            var myCode = new TermNetworkCodeModel();
            myCode.Code = Code.Code;
            myCode.num = 10;


            var data = myCode;
          
          
         
            return Ok(data);
        }




    }
}
