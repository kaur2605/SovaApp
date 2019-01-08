using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class SearchHistoryModel
    {
        public string SearchHistoryUrl { get; set; }
        public string SearchText { get; set; }
        public DateTime SearchDate { get; set; }
        public string RemoveHistory { get; set; }
    }
}
