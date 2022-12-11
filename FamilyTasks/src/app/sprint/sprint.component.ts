import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SprintService } from 'src/app/shared/services/sprint.service';

@Component({
    selector: 'app-sprint',
    templateUrl: 'sprint.component.html',
    styleUrls: ['./sprint.component.scss']
})

export class SprintComponent implements OnInit {
    
    public createMode: boolean = false;

    constructor(public sprintService: SprintService, public router: Router) {
        this.createMode = false;
        this.sprintService.selectedSprintChanged.subscribe(data => {
            this.createMode = data.showCreate;
        });
     }

    ngOnInit() { }

    goToBookingPage() {
        console.log('enters in goto');
        this.router.navigateByUrl("/book");
    }
}