import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-sprint',
    templateUrl: 'sprint.component.html',
    styleUrls: ['./sprint.component.scss']
})

export class SprintComponent implements OnInit {
    
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