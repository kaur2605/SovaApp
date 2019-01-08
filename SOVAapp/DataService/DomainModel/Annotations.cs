using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataService.DomainModel
{
   public class Annotations
    {
        [Key]
        public int AnnotationId { get; set; }
        public int MarkedPostId { get; set; }
        public string Annotation { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
        public Marking Marking { get; set; }

    }
}
