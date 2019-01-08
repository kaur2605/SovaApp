using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;

namespace DataService.DTO
{
    public class SearchHistoryDTO
    {
        public int Id { get; set; }
        public string SearchContent { get; set; }
        public DateTime SearchDate { get; set; }

        public SearchHistoryDTO(int Id, string SearchContent, DateTime SearchDate)
        {
            this.Id = Id;
            this.SearchContent = SearchContent;
            this.SearchDate = SearchDate;

        }
    }
}
