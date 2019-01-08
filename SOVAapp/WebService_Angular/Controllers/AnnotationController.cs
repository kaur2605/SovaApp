using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.DataAccessLayer;
using DataService.DTO;
using WebService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Routing;

namespace WebService.Controllers 
{
    [Route("api/marking/{Pid}/annotation")]
    public class AnnotationController : CustomeController
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;


        public AnnotationController(IRepository _repository, IMapper mapper)
        {
            this._repository = _repository;
            this._mapper = mapper;
        }

        [HttpGet(Name = nameof(GetAnnotration))]
        public IActionResult GetAnnotration(int Pid)
        {
            var annot = _repository.GetAnnotationById(Pid);
            var annotationModel = new AnnotationModel();
            annotationModel.MarkingLink = Url.Link(nameof(MarkingController.GetMarking), new { Pid = annot.MarkedPostId });
            annotationModel.AnnotationText = annot.Annotation;
            annotationModel.EditAnnotation = Url.Link(nameof(AnnotationController.EditAnnotation), new { AnnotId = annot.Annotationid, text = annot.Annotation });
            annotationModel.RemoveAnnotation = Url.Link(nameof(AnnotationController.RemoveAnnotation), new { AnnotId = annot.Annotationid });
            return Ok(annotationModel);


        }
        [HttpPost("", Name = nameof(AddAnnotation))]
        public IActionResult AddAnnotation(int Pid ,[FromBody] AnnotationModel obj) {

          
               var addedAnnotation =  _repository.AddAnnotation(Pid, obj.AnnotationText,(int)obj.From, (int)obj.To);
                var annotationModel = new AnnotationModel();
                annotationModel.MarkingLink = Url.Link(nameof(MarkingController.GetMarking), new { Pid = addedAnnotation.MarkedPostId });
                annotationModel.AnnotationText = addedAnnotation.Annotation;
                annotationModel.From = obj.From;
                annotationModel.To = obj.To;
                annotationModel.EditAnnotation = Url.Link(nameof(AnnotationController.EditAnnotation), new { AnnotId = addedAnnotation.Annotationid, text = addedAnnotation.Annotation });
                annotationModel.RemoveAnnotation = Url.Link(nameof(AnnotationController.RemoveAnnotation), new { AnnotId = addedAnnotation.Annotationid });
                return Created($"api/marking/{Pid}/annotation", annotationModel);
        
        }


        [HttpDelete("{AnnotId}", Name = nameof(RemoveAnnotation))]
        public IActionResult RemoveAnnotation(int AnnotId)
        {

            if (_repository.GetAnnotationById(AnnotId).Annotation == "Empty")
            {
                return NotFound("There's no annotation to delete");
            }
            else
            {
                _repository.DeleteAnnotation(AnnotId);
                return Ok("Annotation has been deleted");
                
            }

        }

        [HttpPut("{AnnotId}", Name = nameof(EditAnnotation))]
        public IActionResult EditAnnotation(int AnnotId, [FromBody] AnnotationModel myObject )
        {

            if (_repository.GetAnnotationById(AnnotId).Annotation == "Empty")
            {
                return NotFound("There's no annotation to Edit");
            }
            else
            {
               var editedAnnotation = _repository.EditAnnotation(AnnotId, myObject.AnnotationText);
                var annotationModel = new AnnotationModel();
                annotationModel.MarkingLink = Url.Link(nameof(MarkingController.GetMarking), new { Pid = editedAnnotation.MarkedPostId });
                annotationModel.AnnotationText = editedAnnotation.Annotation;
                annotationModel.From = editedAnnotation.From;
                annotationModel.To = editedAnnotation.To;
                annotationModel.EditAnnotation = Url.Link(nameof(AnnotationController.EditAnnotation), new { AnnotId = editedAnnotation.Annotationid});
                annotationModel.RemoveAnnotation = Url.Link(nameof(AnnotationController.RemoveAnnotation), new { AnnotId = editedAnnotation.Annotationid });
                return Ok(annotationModel);

            }
        }




    }
}
