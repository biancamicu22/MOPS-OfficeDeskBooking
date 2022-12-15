import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppSettings } from 'src/app/app.settings';
import { BookingModel } from '../models/bookingModel';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  public bookingList: BookingModel[] = [];
  public bookingResult: BookingModel;

  constructor(private appSettings: AppSettings, private http: HttpClient) { }

  public deleteBooking(bookingId: string): Observable<any>{
    return this.http.delete(this.appSettings.baseApiUrl + "booking/" + bookingId);
}

public getAllBookings(): Observable<BookingModel[]>{
    return this.http.get<BookingModel[]>(this.appSettings.baseApiUrl + "booking/all").pipe(map(list => {
        this.bookingList = list;
        return list;
    }));
}

public getUserActiveBookings(userEmail: string): Observable<BookingModel[]>{
    return this.http.get<BookingModel[]>(this.appSettings.baseApiUrl + "booking/my-bookings/" + userEmail).pipe(map(list => {
        this.bookingList = list;
        return list;
    }));
}

public updateBooking(booking: BookingModel): Observable<BookingModel>{
    return this.http.put<BookingModel>(this.appSettings.baseApiUrl + "booking", booking).pipe(map(x => {
        this.bookingResult = x;
        return x;
    }));
}

public createBooking(booking: BookingModel): Observable<BookingModel>{
    return this.http.post<BookingModel>(this.appSettings.baseApiUrl + "booking", booking).pipe(map(x => {
        this.bookingResult = x;
        return x;
    }));
}
}
