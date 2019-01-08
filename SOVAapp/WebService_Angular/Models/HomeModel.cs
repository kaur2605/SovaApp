using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.DTO;
namespace WebService.Models
{
    public class HomeModel
    {
        public string QuestionsUrl{get; set;}
        public string UsersUrl { get; set; }
        public string MarkingsUrl { get; set; }
        public string Search { get; set; }
        public string TermsByPostId { get; set; }
        public string TermNetwrok { get; set; }
        public string SearchHistoryUrl { get; set; }
        public string CustomeFieldUrl { get; set; }

        public List<CustomPostModel> RecommendedQuestions;

    }
}
