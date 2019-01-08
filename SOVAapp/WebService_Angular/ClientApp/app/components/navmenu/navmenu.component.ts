import { Component, Inject } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
})
    
export class NavMenuComponent {
    questionsUrl: string;
    constructor( @Inject('BASE_URL') private baseUrl: string) {
        this.questionsUrl = this.baseUrl + "api/question";
    }
}
