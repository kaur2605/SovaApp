using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;

namespace DataService.DTO
{
    public class MarkingDTO
    {
        public int MarkedPostId { get; set; }
      
        public DateTime MarkingDate { get; set; }
        public  AnnotationsDTO Annotations;

        public MarkingDTO(int MarkedPostId , DateTime MarkingDate, AnnotationsDTO Annotations)
        {
            this.MarkedPostId = MarkedPostId;
            this.MarkingDate = MarkingDate;
            this.Annotations = Annotations;
       
        }

    }
}
