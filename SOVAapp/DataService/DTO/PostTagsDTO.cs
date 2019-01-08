using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace DataService.DTO
{
    public class PostTagsDTO
    {
        [Key]
        public int id { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
   
        public virtual TagsDTO Tag { get; set; }

        public PostTagsDTO (int PostId, int TagId, TagsDTO Tag) 
        {
            this.PostId = PostId;
            this.TagId = TagId;
       
            this.Tag = Tag;

        }

    }
}
