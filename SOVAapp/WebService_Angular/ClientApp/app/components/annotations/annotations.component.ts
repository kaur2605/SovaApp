import { Component, Inject, NgModule, OnInit, Input, Injectable} from '@angular/core';
import { Http, Headers, RequestOptions} from '@angular/http';
import { Routes, ActivatedRoute, Router, NavigationEnd} from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
@NgModule({
    declarations: [AnnotationsComponent],
    bootstrap: [AnnotationsComponent]
})

@Component({
    selector: 'annotations',
    templateUrl: './annotations.component.html',

})

export class AnnotationsComponent implements OnInit{
 
    public annotations: GetAnnotations[];
    currentEdit: string = "null";
    isNewAnnotation: boolean = false;
    annotationBody: string = "";

    @Input('parentdata') Url: string;
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute, private router: Router) {

 

    }

    ngOnInit() {
        this.getAnnotationsData().subscribe(result => {

            this.annotations = result.json() as GetAnnotations[];
        }, error => console.error(error));

    }

    getAnnotationsData() {
        return this.http.get(this.Url);
    }

   startEditing(editAnnotation:string) {
    this.currentEdit= editAnnotation;
    }
    
   addAnottation() {
    this.annotationBody="";
    this.isNewAnnotation = true;
     }

   abortAnnotation() {
       this.annotationBody = "";
       this.isNewAnnotation = false;
     }

    
   createAnnotation(addUrl:string) {
    
       var body = JSON.stringify({

           annotationText: this.annotationBody,
           From: 0,
           To: 0
       });
       let headers = new Headers({ 'Content-Type': 'application/json' }); // ... Set content type to JSON
       let options = new RequestOptions({ headers: headers, body: body }); // Create a request option


       this.http
           .post(addUrl,
           body, options)
           .subscribe(data => {
               this.isNewAnnotation=false;

               this.getAnnotationsData().subscribe(result => {
                   this.annotations = result.json() as GetAnnotations[];               
               }, error => console.error(error));

           }, error => {
               console.log(JSON.stringify(error.json()));
           });

   }


   deleteAnnotation(removeAnnotation:string) {
    var deleteAnotationUrl = removeAnnotation;
    var body = "";
    this.http
        .delete(deleteAnotationUrl,
        body)
        .subscribe(data => {
            this.getAnnotationsData().subscribe(result => {

                this.annotations = result.json() as GetAnnotations[];
            }, error => console.error(error));

        }, error => {
            console.log(JSON.stringify(error.json()));
        });


    }
    
   editAnnotation(myEditedText: string, editUrl: string) {

       var body = JSON.stringify({

           annotationText: myEditedText,
           From: 0,
           To: 0
       });
       let headers = new Headers({ 'Content-Type': 'application/json' }); // ... Set content type to JSON
       let options = new RequestOptions({ headers: headers, body: body }); // Create a request option

       this.http
           .put(editUrl,
           body, options)
        .subscribe(data => {
            this.getAnnotationsData().subscribe(result => {
                this.currentEdit = "";

                this.annotations = result.json() as GetAnnotations[];
            }, error => console.error(error));

        }, error => {
            console.log(JSON.stringify(error.json()));
        });



}




}

 
interface GetAnnotations {
    annotationText:string
}
