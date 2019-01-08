using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.DomainModel
{
    public class CoOccurrence
    {
        [Key]
        public string Word { get; set; }
        [Key]
        public string Word2 { get; set; }
        public int Grade { get; set; }

    }
}
