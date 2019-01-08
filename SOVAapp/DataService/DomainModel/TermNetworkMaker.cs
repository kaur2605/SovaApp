using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.DomainModel
{
    public class TermNetworkMaker
    {
        [Key]
        public string Raw { get; set; }
    }
}
