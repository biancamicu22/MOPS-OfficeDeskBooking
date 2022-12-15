import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationComponent } from './authentication/authentication.component';
import { BookingComponent } from './booking/booking.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';
import { Roles } from './shared/models/roles';
import { AuthGuard } from './shared/services/auth.guard';
import { SprintComponent } from './sprint/sprint.component';
import { UserDetailsComponent } from './user-details/user-details.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: "/auth",
    pathMatch: 'full'
  },
  {
    path: 'home', 
    component: SprintComponent,
    canActivate: [AuthGuard],
    data: { role: [Roles.User], requireLogin: true },
  },
  {
    path: 'auth', 
    component: AuthenticationComponent,
  },
  {
    path: 'account',
    component: UserDetailsComponent,
    canActivate: [AuthGuard],
    data: { role: [Roles.User], requireLogin: true },
  },
  {
    path: 'book',
    component: BookingComponent,
    canActivate: [AuthGuard],
    data: { role: [Roles.User], requireLogin: true },
  },
  {
    path: 'my-bookings',
    component: MyBookingsComponent,
    canActivate: [AuthGuard],
    data: { role: [Roles.User], requireLogin: true},
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }