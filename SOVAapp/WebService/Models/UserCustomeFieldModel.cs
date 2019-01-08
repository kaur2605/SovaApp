using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.DTO;

namespace WebService.Models
{
    public class UserCustomeFieldModel
    {
        public int postLimit;
        public ICollection<String> FavortieTags;
        public DateTime CreationDate;
        public String MakeNewCustomization;
    }

}
