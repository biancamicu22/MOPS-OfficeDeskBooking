import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { SharedModule } from './shared/shared.module';
import { AuthenticationComponent } from './authentication/authentication.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AppSettings } from './app.settings';
import { UserService } from './shared/services/user.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule, DatePipe } from '@angular/common';
import { JwtInterceptor } from './shared/services/jwt.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { ToastrModule } from 'ngx-toastr';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { CustomLoaderService } from './shared/services/customLoader.service';
import { HomeComponent } from './home/home.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { BookingComponent } from './booking/booking.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthenticationComponent,
    HomeComponent,
    UserDetailsComponent,
    BookingComponent,
    MyBookingsComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    NgxUiLoaderModule,
    MDBBootstrapModule.forRoot(),
    JwtModule.forRoot({
      config: {
        blacklistedRoutes: []
      }
    }),
    NgbModule
  ],
  providers:  [
    DatePipe,
    AppSettings,
    CustomLoaderService,
    UserService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }