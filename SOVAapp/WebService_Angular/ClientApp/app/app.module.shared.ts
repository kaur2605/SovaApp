import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { QuestionsComponent } from './components/questions/questions.component';
import { QuestionComponent } from './components/question/question.component';
import { CommentsComponent } from './components/comments/comments.component';
import { AnswersComponent } from './components/answers/answers.component';
import { UsersComponent } from './components/users/users.component';
import { MarkingsComponent } from './components/markings/markings.component';
import { AnnotationsComponent } from './components/annotations/annotations.component';
import { CustomizationComponent } from './components/customization/customization.component';
import { SearchComponent } from './components/search/search.component';
import { JcloudComponent } from './components/jcloud/jcloud.component';





@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        QuestionComponent,
        QuestionsComponent,
        HomeComponent,
        SearchComponent,
        CommentsComponent,
        AnswersComponent,
        MarkingsComponent,
        AnnotationsComponent,
        CustomizationComponent,
        UsersComponent,
        JcloudComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'question/:id', component: QuestionComponent },
            { path: 'search', component: SearchComponent },
            { path: 'questions', component: QuestionsComponent },
            { path: 'users', component: UsersComponent },
            { path: 'markings', component: MarkingsComponent },
            { path: 'customization', component: CustomizationComponent },
            { path: '**', redirectTo: 'home' }
            
        ])
    ]
})
export class AppModuleShared {
}
