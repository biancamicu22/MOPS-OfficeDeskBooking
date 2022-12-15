import { BookingModel } from "./bookingModel";

export class DeskModel{
    public deskNumber: number;
    public numberOfMonitors: number;
    public bookings: BookingModel[];

    constructor(init: Partial<DeskModel>) {
        Object.assign(this, init);
    }
}