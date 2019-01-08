using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.DTO;
namespace WebService.Models
{
    public class QuestionModel
    {
        public int PostId { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<String> Tags;
        public List<LinkedPostsModel> LinkedPosts;
        public string UserUrl { get; set; }
        public string AcceptedAnswerUrl { get; set; }
        public string AnswersUrl { get; set; }
        public string CommentsUrl { get; set; }
        public string MarkThisPost { get; set; }
        public string UnMarkPost { get; set; }

    }
}
