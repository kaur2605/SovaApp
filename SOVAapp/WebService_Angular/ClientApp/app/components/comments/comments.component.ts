import { NgModule, Input, Inject, Component, OnInit} from '@angular/core';
import { Http } from '@angular/http';
import { Routes } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';


//npm install --save jquery
//npm install - D @types/jquery
@NgModule({
    declarations: [CommentsComponent],
    bootstrap: [CommentsComponent]

})
@Component({
    selector: 'comments',
    templateUrl: './comments.component.html'
})
export class CommentsComponent implements OnInit {
    @Input('parentdata') parentdata:string ;

    public comments: GetComments[];

    mylink: string;

    constructor(private http:Http,@Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
    }



     

    ngOnInit() {
        this.getCommentsData().subscribe(result => {
            this.comments = result.json() as GetComments[];
        }, error => console.error(error));
    }
    getCommentsData() {

        return this.http.get(this.parentdata);
    }



}

interface GetComments {
}

