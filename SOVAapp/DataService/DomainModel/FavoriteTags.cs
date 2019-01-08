using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.DomainModel
{
    public class FavoriteTags
    {
        [Key]
        public int Id { get; set; }
        public int UserCustomeFieldId { get; set; }
        public int TagId { get; set; }
        public virtual UserCustomeField UserCustomeField { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
