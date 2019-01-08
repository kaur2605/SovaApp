import { Component, Inject,Input,OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';

@Component({
    selector: 'questions',
    templateUrl: './questions.component.html'
})
export class QuestionsComponent implements OnInit {
    public questions: GetQuestions;

    url = 'api/question?page=' + this.route.snapshot.queryParams["page"] + "&pageSize=12";
    Url = this.route.snapshot.queryParams["url"];
    markingStatus: string;

    hasStarted: boolean = false;


    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute, private router: Router) {
        

        router.events
            .subscribe((event) => {
                if (this.hasStarted){
                if (event instanceof NavigationEnd) {
                    var pageNumber = this.route.snapshot.queryParams["page"];
                    console.log(pageNumber);
                    http.get(baseUrl + 'api/question?page=' + pageNumber + "&pageSize=12"
                    ).subscribe(result => {
                        this.questions = result.json() as GetQuestions;
                        }, error => console.error(error));
                }
                }

            });

    }

    ngOnInit() {

        this.http.get(this.Url).subscribe(result => {
            this.questions = result.json() as GetQuestions;
            this.hasStarted = true;
        }, error => console.error(error));
    }

    public goToNextPage(url: string, pageNum: number) {
        this.url = url;
        this.router.navigate(['/questions'], { queryParams: { page: pageNum + 1 } });
    }

    public goToPrevPage(url: string, pageNum: number) {
        this.url = url;
        this.router.navigate(['/questions'], { queryParams: { page: pageNum - 1 } });
    }

    public goToQuestion(id: number) {
        this.router.navigate(['/question', id]);
    }

    body: string = "";

}

interface GetQuestions {
    page: number;
}

