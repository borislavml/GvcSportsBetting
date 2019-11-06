import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { SportEventsComponent } from './sport-events/sport-events.component';

@NgModule({
    declarations: [
        AppComponent,
        SportEventsComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
        ])
    ],
    providers: [
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
