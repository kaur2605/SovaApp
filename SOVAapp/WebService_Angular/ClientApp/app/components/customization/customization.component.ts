import { Component, Inject, NgModule, OnInit } from '@angular/core';
import { Http, RequestOptions,Headers } from '@angular/http';
import { Routes, ActivatedRoute, Router, NavigationEnd} from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
@NgModule({
    declarations: [CustomizationComponent],
    bootstrap: [CustomizationComponent]
})

@Component({
    selector: 'customization',
    templateUrl: './customization.component.html',

})

export class CustomizationComponent implements OnInit{

    myCustomizationUrl: string;
    startedEditingLimit: boolean = false; 
    startedEditingTags: boolean = false; 
    newPostlimit: number;
    newCustomeTags: string;
    loading: boolean = true;

    public customization: GetCustomization[];
   // public sampleData: any;

    url = 'api/customization/';

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute, private router: Router) {


        router.events
            .subscribe((event) => {

                if (event instanceof NavigationEnd) {

                    http.get(baseUrl + this.url
                    ).subscribe(result => {

                        this.customization = result.json() as GetCustomization[];
                        this.loading = false;
                        }, error => console.error(error));
                }

            });

    }

ngOnInit() {

    }

    currentpostLimitToedit(currentPostLimit: number) {
    if (!this.startedEditingLimit) {
        this.newPostlimit = 12;
        this.startedEditingLimit = true;
    }
}
    currentTagsToedit(currentFavortieTags: string[]) {
        if (!this.startedEditingTags) {
            this.newCustomeTags = currentFavortieTags.join(',');
        this.startedEditingTags = true;
    }
    }
    saveCustomeField() {
     
        var NewCustomeUrl = this.baseUrl + "api/customization/";
        var body = JSON.stringify({
            FavortieTags: this.newCustomeTags.split(","),
            PostLimit: this.newPostlimit
        });
        let headers = new Headers({ 'Content-Type': 'application/json' }); // ... Set content type to JSON
        let options = new RequestOptions({ headers: headers, body: body }); // Create a request option
        this.loading = true;

        this.http
            .post(NewCustomeUrl, body, options
            )
            .subscribe(data => {

                this.http.get(this.baseUrl + this.url
                ).subscribe(result => {
                    this.loading = false;

                    this.customization = result.json() as GetCustomization[];

                }, error => console.error(error));

            }, error => {
                console.log(JSON.stringify(error.json()));
            });


}



}

 
interface GetCustomization {
}
