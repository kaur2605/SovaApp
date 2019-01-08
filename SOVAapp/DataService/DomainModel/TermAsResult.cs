using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.DomainModel
{
    public class TermAsResult
    {
        [Key]
        public string Word { get; set; }
        public decimal Score { get; set; }
    }
}
