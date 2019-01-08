using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.DomainModel
{
    public class SearchHistory
    {
        public int Id { get; set; }
        public string SearchContent { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
