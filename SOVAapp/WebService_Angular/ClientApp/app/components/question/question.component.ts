import { Component, Inject, NgModule, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Routes, ActivatedRoute, Router, NavigationEnd} from '@angular/router';
import { CommentsComponent } from '../comments/comments.component';
import { AnswersComponent } from '../answers/answers.component';
import { AnnotationsComponent } from '../Annotations/Annotations.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
@NgModule({
    declarations: [QuestionComponent, CommentsComponent, AnswersComponent, AnnotationsComponent],
    bootstrap: [QuestionComponent]
})

@Component({
    selector: 'question',
    templateUrl: './question.component.html',

})

export class QuestionComponent implements OnInit{
    questionReady: boolean = false;
    isMarked: boolean = false;
    myAnnotationUrl: string;

    public question: GetQuestion[];
   // public sampleData: any;

    url = 'api/question/' + this.route.snapshot.paramMap.get('id');
    newId:number = 0;
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute, private router: Router) {

        this.myAnnotationUrl = baseUrl + 'api/marking/' + this.route.snapshot.paramMap.get('id');

        router.events
            .subscribe((event) => {

                if (event instanceof NavigationEnd) {
                    this.questionReady = false;

                    http.get(baseUrl + this.url
                    ).subscribe(result => {

                        this.question = result.json() as GetQuestion[];
                        this.questionReady = true;
                        this.isMarked = result.json().unMarkPost != "Not marked yet";

                        }, error => console.error(error));
                }

            });

    }

ngOnInit() {

    }



    public goToQuestion(id: number) {
        //this.router.navigate(['']);
        this.router.navigate(['/question', id]);
        this.url = 'api/question/' + id;

}


    public markThis() {
        var AddMarkingUrl = "/api/marking/" + this.route.snapshot.paramMap.get('id');
        var body = "";
        this.http
            .post(AddMarkingUrl,
            body)
            .subscribe(data => {

                this.isMarked = true;

            }, error => {
                console.log(JSON.stringify(error.json()));
            });


    }

    public unMarkThis() {
        var deleteMarkingUrl = "/api/marking/" + this.route.snapshot.paramMap.get('id');
        var body = "";
        this.http
            .delete(deleteMarkingUrl,
            body)
            .subscribe(data => {
                this.isMarked = false;

            }, error => {
                console.log(JSON.stringify(error.json()));
            });


    }


}

 
interface GetQuestion {
    commentsUrl: string
}
