import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    public suggestedQuestions: GetsuggestedQuestions;
    public customization: GetCustomization;

    suggestedQurl = 'api/start';
    customizationUrl = 'api/customization';
    markingStatus: string;
    postId: number = 1;
    QNotloaded: boolean=true;

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute, private router: Router) {

    }

    ngOnInit() {

        this.http.get(this.baseUrl + this.suggestedQurl).subscribe(result => {
            this.suggestedQuestions = result.json().recommendedQuestions as GetsuggestedQuestions;
            this.QNotloaded = false;
        }, error => console.error(error));

        this.http.get(this.baseUrl + this.customizationUrl).subscribe(result => {
            this.customization = result.json() as GetCustomization;
        }, error => console.error(error));
    }

    public goToQuestion(id: number) {
        this.router.navigate(['/question', id]);
    }
}

interface GetsuggestedQuestions {
}
interface GetCustomization {
}
