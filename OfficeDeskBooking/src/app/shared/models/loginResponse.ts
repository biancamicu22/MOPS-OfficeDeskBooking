export class LoginResponse{
    public username: string;
    public email: string;
    public jwt: string;
    public firstName: string;
    public lastName: string;
    public token: string;
    public birthDate: string;
    public phoneNumber: string;
    public role: string;

    constructor(init: Partial<LoginResponse>) {
        Object.assign(this, init);
    }
}