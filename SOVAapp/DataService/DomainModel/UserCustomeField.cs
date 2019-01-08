using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.DomainModel
{
    public class UserCustomeField
    {
        public int Id { get; set; }
        public int Postlimit { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<FavoriteTags> FavoriteTags;
    }
}
