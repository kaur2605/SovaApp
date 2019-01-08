using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class CustomPostModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string QuestionUrl { get; set; }
        public string User { get; set; }
        public int? Score { get; set; }
        public ICollection<string> Tags;


    }
}
