import { Component, OnInit } from '@angular/core';
import { BookingModel } from '../shared/models/bookingModel';
import { LoginResponse } from '../shared/models/loginResponse';
import { BookingService } from '../shared/services/booking.service';
import { CustomLoaderService } from '../shared/services/customLoader.service';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.scss']
})
export class MyBookingsComponent implements OnInit {

  myCurrentBookings: BookingModel[] = [];
  user: LoginResponse;

  constructor(public bookingService: BookingService, private customService: CustomLoaderService, public userService: UserService) {
    this.customService.start();
    this.user = this.userService.GetUserData();
    this.bookingService.getUserActiveBookings(this.user.email).subscribe(resp => {
      console.log(resp);
      this.myCurrentBookings = resp;
      this.customService.stop();
    }, this.customService.errorFromResp);
   }

  ngOnInit(): void {
  }

  cancelBooking(bookingId: string){
    this.customService.start();
    this.bookingService.deleteBooking(bookingId).subscribe(res => {
      this.customService.success('Rezervarea a fost anulata cu succes!', 'Success');
      window.location.reload();
      this.customService.stop();
    })
  }

}
