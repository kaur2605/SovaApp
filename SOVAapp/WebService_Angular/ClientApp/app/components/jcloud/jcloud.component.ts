import { Component, Inject, Input, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
declare var jquery: any;
declare var $: any;

@Component({
    selector: 'jcloud',
    templateUrl: './jcloud.component.html'
})
export class JcloudComponent implements OnInit {
    public jclould: GetJCloud;


    @Input("parentdata") id: string;
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute, private router: Router) {
    }


    ngOnInit() {
        var CssId = "#" + this.id ;
        var Url = this.baseUrl + 'api/TermAsResult/' + this.id;
        this.http.get(Url).subscribe(result => {
            this.jclould = result.json() as GetJCloud;
            $(CssId).jQCloud(this.jclould);
        }, error => console.error(error));

    }
}


interface GetJCloud {
}

