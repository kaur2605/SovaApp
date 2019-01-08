using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;

namespace DataService.DTO
{
    public class TagsDTO
    {
        public int Id { get; set; }
        public String Tag { get; set; }


        public TagsDTO(int Id, String Tag)
        {
            this.Id = Id;
            this.Tag = Tag;


        }
    }
}
