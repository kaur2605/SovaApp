using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class AnnotationModel
    {
        public string MarkingLink { get; set; }
        public string AnnotationText { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
        public string EditAnnotation { get; set; }
        public string RemoveAnnotation { get; set; }
        
    }
}
