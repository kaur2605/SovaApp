import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';

@Component({
    selector: 'users',
    templateUrl: './users.component.html'
})
export class UsersComponent {
    public users: GetUsers;

    url = 'api/user?page=' + this.route.snapshot.queryParams["page"] + "&pageSize=12";





    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute, private router: Router) {
        http.get(baseUrl + this.url).subscribe(result => {
            this.users = result.json() as GetUsers;
        }, error => console.error(error));

        router.events
            .subscribe((event) => {
                
                if (event instanceof NavigationEnd) {
                    this.users = {};
                     var pageNumber = this.route.snapshot.queryParams["page"];
                     console.log(pageNumber);
                     http.get(baseUrl + 'api/user?page=' + pageNumber + "&pageSize=12"
                     ).subscribe(result => {
                         this.users = result.json() as GetUsers;
                     }, error => console.error(error));
                }

            });

    }



    public goToNextPage(url: string,pageNum:number) {
        this.url = url;
        this.router.navigate(['/users'], { queryParams: { page: pageNum + 1} });
    }

    public goToPrevPage(url: string, pageNum: number) {
        this.url = url;
        this.router.navigate(['/users'], { queryParams: { page: pageNum - 1 } });
    }
    
    public goToQuestion(id:number) {
        this.router.navigate(['/question', id]);     
    }


}


interface GetUsers {
}

