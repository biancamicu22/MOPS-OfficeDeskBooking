import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html',
    styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
    
    public createMode: boolean = false;

    constructor(public router: Router) {
        this.createMode = false;
     }

    ngOnInit() { }

    goToBookingPage() {
        console.log('enters in goto');
        this.router.navigateByUrl("/book");
    }
}