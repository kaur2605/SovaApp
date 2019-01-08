using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class SearchModel
    {
        public string SearchText;
        public int PostId;
        public string PostUrl;
        public string PostTitle;
        public string jCloudUrl;
        //  public string PostBody;
        //public double? Score;
        public List<string> Tags;
         
    }
}
