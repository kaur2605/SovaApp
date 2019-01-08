using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;

namespace DataService.DTO
{
    public class CustomePostsDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int PostTypeId { get; set; }
        public int? Score { get; set; }
        public int? totalResults { get; set; }
        public CustomePostsDTO(int PostId, string Title, string Body, int UserId , int PostTypeId)
        {
            this.PostId = PostId;
            this.Title = Title;
            this.Body = Body;
            this.UserId = UserId;
            this.PostTypeId = PostTypeId;

        }
    }
}
