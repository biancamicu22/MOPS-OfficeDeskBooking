import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { UserModel } from '../shared/models/userModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DatePipe, ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {

  user: UserModel;
  decodedRole: string = "";
  userLabel = "user";
  editUserPassword: FormGroup;
  editUserDetails: FormGroup;
  submitFlag : boolean = false;
  submitFlagNewUser: boolean = false;
  userBirthDateString : string = '';
  userCreatedAtDateString : string = '';
  orgStartedDateString : string = '';
  orgCreatedDateString : string = '';
  successfullyUpdatedPasswordUser: boolean = false;
  wrongUpdatedPasswordUser: boolean = false;
  errors = null;

  constructor(public userService: UserService, private router: Router, private fb: FormBuilder,public datepipe: DatePipe, private scroll: ViewportScroller) { }

  ngOnInit(): void {
    if(this.userService.currentUserValue != null) {
      this.user = new UserModel(this.userService.currentUserValue);
      console.log(this.user);
       this.userBirthDateString = this.datepipe.transform(this.user.birthdate, 'dd/MM/yyyy');
        console.log(this.userBirthDateString);
       this.initializeUserForm(this.user);
       this.initializeUserPasswordForm();
    }
    
  }

  checkInput(input: any) {
    return input.invalid && (input.dirty || input.touched);
  }

  initializeUserForm(currentUser: UserModel) {
    this.editUserDetails = this.fb.group({
      firstName: [currentUser.firstName],
      lastName: [currentUser.lastName],
      email: [currentUser.email],
      birthdate: [this.userBirthDateString],
      phoneNumber: [currentUser.phoneNumber]
    });
  }

  initializeUserPasswordForm() {
    this.editUserPassword = this.fb.group({
      currentPassword: ['', [Validators.required]],
      newPassword:['', [Validators.required]],
      confirmNewPassword:['', [Validators.required]]
    })
  }
  get g() { return this.editUserPassword.controls; }

  editPassword() {
    console.log('before null check');
    if (this.userService.currentUserValue != null) {
      if(this.editUserPassword.value.currentPassword != null && this.editUserPassword.value.newPassword != null && this.editUserPassword.value.confirmNewPassword != null) {
        console.log('after null check');
        if(this.editUserPassword.value.newPassword == this.editUserPassword.value.confirmNewPassword) {
          this.submitFlagNewUser = false;
          this.userService.updateUserPassword(this.editUserPassword.value.currentPassword, this.editUserPassword.value.newPassword).subscribe({ next: r=> {
            console.log(r);
            this.successfullyUpdatedPasswordUser = true;
            this.scroll.scrollToPosition([0,0]);
            setTimeout(()=>{this.successfullyUpdatedPasswordUser = false; window.location.reload();}, 3000);
          },
          error: error => {
            this.errors = error;
            this.wrongUpdatedPasswordUser = true;
            this.scroll.scrollToPosition([0,0]);
            setTimeout(()=>{this.wrongUpdatedPasswordUser = false;}, 3000);
          }});
        } else {
          this.submitFlagNewUser = true;
        }
      }
    }
  }

}
