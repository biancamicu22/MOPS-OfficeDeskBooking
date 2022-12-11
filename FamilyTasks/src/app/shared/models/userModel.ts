export class UserModel{
    public firstName: string;
    public lastName: string;
    public email: string;
    public username: string;
    public phoneNumber: string;
    public department: string;
    public password: string;
    public id: string;
    public birthdate: Date;
    
    constructor(init: Partial<UserModel>) {
        Object.assign(this, init);
    }
}