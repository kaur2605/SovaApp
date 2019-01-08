using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class MarkingModel
    {
        public string MarkingUrl { get; set; }
        public string RemoveMarking { get; set; }
        public string PostUrl { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public ICollection<AnnotationModel> MarkingAnnotation { get; set; }
        public DateTime MarkedDate { get; set; }

        public string AddAnnotation { get; set; }



    }
}
