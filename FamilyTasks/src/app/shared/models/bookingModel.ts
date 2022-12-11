import { DeskModel } from "./deskModel";
import { UserModel } from "./userModel";

export class BookingModel{
    public startDate: Date;
    public endDate: Date;
    public deskNumber: number;
    public id: string;
    public user_id: string;

    constructor(init: Partial<BookingModel>) {
        Object.assign(this, init);
    }
}