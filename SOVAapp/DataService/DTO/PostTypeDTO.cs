using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;

namespace DataService.DTO
{
    public class PostTypeDTO
    {

        public int Id { get; set; }
        public String Type { get; set; }

        public PostTypeDTO(int Id, String Type)
        {
            this.Id = Id;
            this.Type = Type;

        }
    }
}
