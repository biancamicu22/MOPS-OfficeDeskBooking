import { JsonPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbDateStruct, NgbCalendar, NgbDatepickerModule, NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { toJSDate } from '@ng-bootstrap/ng-bootstrap/datepicker/ngb-calendar';
import { throws } from 'assert';
import { BehaviorSubject } from 'rxjs';
import { CustomLoaderService } from 'src/app/shared/services/customLoader.service';
import { BookingModel } from '../shared/models/bookingModel';
import { LoginResponse } from '../shared/models/loginResponse';
import { UserModel } from '../shared/models/userModel';
import { BookingService } from '../shared/services/booking.service';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']
})
export class BookingComponent implements OnInit {

  examples = [{name: 0}, {name: 1}, {name: 2}];
  hoveredDate: NgbDate | null = null;
	fromDate: NgbDate | null = null;
	toDate: NgbDate | null = null;
  showStep1$ = new BehaviorSubject<boolean>(false);
  showStep2$ = new BehaviorSubject<boolean>(false);
  showStep3$ = new BehaviorSubject<boolean>(false);
  error$ = new BehaviorSubject<boolean>(false);
  monitor = null;
  user: LoginResponse;

  constructor(private calendar: NgbCalendar, private router: Router, public bookingService: BookingService, private customService: CustomLoaderService, public userService: UserService) { 
    this.fromDate = calendar.getToday();
		this.toDate = calendar.getNext(calendar.getToday(), 'd', 1);
    this.error$.subscribe((error) => {
      console.log('error ' + error);
    })
  }

  ngOnInit(): void {
    this.showStep1$.next(true);
  }

  onDateSelection(date: NgbDate) {
		if (!this.fromDate && !this.toDate) {
			this.fromDate = date;
		} else if (this.fromDate && !this.toDate && (date.after(this.fromDate) || date.equals(this.fromDate))) {
			this.toDate = date;
		} else {
			this.toDate = null;
			this.fromDate = date;
		}
	}

	isHovered(date: NgbDate) {
		return (
			this.fromDate && !this.toDate && this.hoveredDate && date.after(this.fromDate) && date.before(this.hoveredDate)
		);
	}

	isInside(date: NgbDate) {
		return this.toDate && date.after(this.fromDate) && date.before(this.toDate);
	}

	isRange(date: NgbDate) {
		return (
			date.equals(this.fromDate) ||
			(this.toDate && date.equals(this.toDate)) ||
			this.isInside(date) ||
			this.isHovered(date)
		);
	}

  public goToSecondStep = (event: Event): void => {
    if(this.fromDate != null && this.toDate != null) {
      this.error$.next(false);
      this.showStep1$.next(false);
      this.showStep2$.next(true);
      this.showStep3$.next(false);

    } else {
      this.error$.next(true);
      console.log(this.error$);
    }
  }

  public goBackToStepOne = (event: Event): void => {
    this.error$.next(false);
    this.showStep1$.next(true);
    this.showStep2$.next(false);
    this.showStep3$.next(false);
  }

  public goBackToStepTwo = (event: Event): void => {
    this.error$.next(false);
    this.showStep1$.next(false);
    this.showStep2$.next(true);
    this.showStep3$.next(false);
  }

  public goToThirdStep = (event: Event): void => {
    if(this.monitor != null) {
      console.log('goto step3 ' + this.monitor);
      this.error$.next(false);
      this.showStep1$.next(false);
      this.showStep2$.next(false);
      this.showStep3$.next(true);

    } else {
      this.error$.next(true);
      console.log(this.error$);
    }
  }

  submitBooking() {
    this.customService.start();
    this.user = this.userService.GetUserData();
    console.log(this.user);
    console.log(this.fromDate.year, ' ', this.fromDate.month, ' ', this.fromDate.day);
    console.log(this.toDate);
    this.bookingService.createBooking(<BookingModel>{
      startDate: new Date(this.fromDate.year, this.fromDate.month - 1, this.fromDate.day, 2, 0, 0),
      endDate: new Date(this.toDate.year, this.toDate.month-1, this.toDate.day, 2, 0, 0),
      user_id: this.user.email,
      deskNumber: this.monitor
    }).subscribe(response => {
      if(response) {
        this.customService.success('Rezervarea a fost efectuata cu succes!', 'Success');
        this.router.navigate(["/home"]);
        this.customService.stop();
      } else {
        this.customService.error('Nu mai exista locuri disponibile in perioada aleasa! Incercati din nou!', 'Error');
        this.router.navigate(["/home"]);
        this.customService.stop();
      }
      
    });
  }

}
