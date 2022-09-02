import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatModule } from './shared/mat.module';
import { AppRoutingModule } from './app-routing.module';
import { AccountModule } from './account/account.module';
import { LoginModule } from './login/login.module';
import { ContentModule } from './content/content.module';
import { MeetingModule } from './meeting/meeting.module';
import { NavigationModule } from './navigation/navigation.module';
import { JwtInterceptor } from './_core/_interceptors/jwt.interceptor';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    MatModule,
    AppRoutingModule,
    AccountModule,
    LoginModule,
    ContentModule,
    MeetingModule,
    NavigationModule
  ],
  exports: [],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
