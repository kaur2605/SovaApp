using System;
using System.Collections.Generic;
using System.Text;
using DataService.DomainModel;


namespace DataService.DTO
{
   public class AnnotationsDTO
    {
        public int Annotationid { get; set; }
        public int MarkedPostId { get; set; }
        public String Annotation { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
        public Marking Marking { get; set; }

        public AnnotationsDTO( int Annotationid, int MarkedPostId,  String Annotation , Marking Marking , int? From, int? To)
        {
            this.Annotationid = Annotationid;
            this.MarkedPostId = MarkedPostId;
            this.Annotation = Annotation;
            this.Marking = Marking;
            this.From = From;
            this.To = To;
        }
    }
}
