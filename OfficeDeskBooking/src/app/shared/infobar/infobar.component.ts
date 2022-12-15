import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
    selector: 'app-infobar',
    templateUrl: 'infobar.component.html'
})

export class InfobarComponent implements OnInit {

    @Input()
    public Route: string = '';
    @Input()
    public BackButtonValue: string = '';

    constructor(public userService: UserService) {
    }
    ngOnInit() { }
}