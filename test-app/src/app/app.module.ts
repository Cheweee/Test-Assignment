import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { FlexLayoutModule } from "@angular/flex-layout";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InstructorsComponent } from './components/instructors/instructors.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './materual.module';
import { ApiUrlInterceptor } from './interceptors/url.interceptor';
import { InstructorComponent } from './components/instructor/instructor.component';
import { FormsModule } from '@angular/forms';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    InstructorsComponent,
    InstructorComponent,
    ConfirmDialogComponent
  ],
  imports: [
    FlexLayoutModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    FormsModule
  ],
  entryComponents: [ 
    InstructorComponent, 
    InstructorsComponent,
    ConfirmDialogComponent
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: ApiUrlInterceptor,
    multi: true
  }],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
