using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace DataService.DomainModel
{
    public class PostTag
    {
      [Key]
        public int PostTagId { get; set; }

        public int PostId { get; set; }
        public int TagId { get; set; }
 
        public virtual Tags Tag { get; set; }
    }
}
