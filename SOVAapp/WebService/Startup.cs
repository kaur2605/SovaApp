using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DataService.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using WebService.Models;
using AutoMapper;
using DataService.DTO;


namespace WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IRepository, RepositoryBody>();
            services.AddSingleton<IMapper>(CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseFileServer();
            app.UseMvc();
        }
        public IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnswerDTO, AnswerModel>()
                    .ReverseMap();
                cfg.CreateMap<UserInfoDTO, UserInfoModel>()
                  .ReverseMap();
                cfg.CreateMap<QuestionDTO, QuestionModel>().
                ForMember(c => c.LinkedPosts, option => option.Ignore())
              .ReverseMap();
                 cfg.CreateMap<CommentDTO, CommentModel>()
             .ReverseMap();
            });



            return config.CreateMapper();
        }
    }
}
