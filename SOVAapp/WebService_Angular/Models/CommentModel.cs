using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class CommentModel
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserUrl { get; set; }
        public string PostUrl { get; set; }
  


    }
}