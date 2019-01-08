using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.DomainModel
{
   public class SearchResult
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
       // public string Body { get; set; }
        public double Rank { get; set; }
     
    }
}
