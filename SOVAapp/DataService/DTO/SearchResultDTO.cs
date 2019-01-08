using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.DTO
{
    public class SearchResultDTO
    {

        public int Id;
        public string Title;
        public double Rank;
        public int? totalResults { get; set; }
        public ICollection<PostTagsDTO> Tags;
        public SearchResultDTO(int Id, string Title, double Rank , ICollection<PostTagsDTO> Tags)
        {
            this.Id = Id;
            this.Title = Title;
            this.Rank = Rank;
            this.Tags = Tags;
        }
    }
}
