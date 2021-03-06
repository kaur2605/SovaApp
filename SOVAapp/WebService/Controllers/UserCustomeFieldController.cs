﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("api/customization")]
    public class UserCustomeFieldController : CustomeController
    {


        private readonly IRepository _repository;
        private readonly IMapper _mapper;


        public UserCustomeFieldController(IRepository _repository, IMapper mapper)
        {
            this._repository = _repository;
            this._mapper = mapper;

        }

        [HttpGet(Name = nameof(GetUserCustomeField))]

        public IActionResult GetUserCustomeField()
        {
            var c = _repository.GetLatestUserCustomeField();
            if (c != null)
            {
                var model = new UserCustomeFieldModel();
                model.postLimit = c.Postlimit;
                model.FavortieTags = c.FavoriteTags.Select(x => x.Tag.Tag).ToList();
                model.CreationDate = c.CreationDate;
                model.MakeNewCustomization = Url.Link(nameof(NewUserCustomeField), new { postlimit = 5, tags = "java,php,laravel" });
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPost("{postlimit}_{tags}", Name = nameof(NewUserCustomeField))]
        public IActionResult NewUserCustomeField(int postlimit, string tags)
        {
            _repository.AddUserCustomeField(postlimit, tags);
            return Created($"api/customization","Updated the new Customization");

        }


        }
}
