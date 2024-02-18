import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AtmService } from '../atm.service';

@Component({
    selector: 'atm-logout',
    templateUrl: './atm-logout.component.html',
    encapsulation: ViewEncapsulation.None
})
export class AtmLogoutComponent {

    constructor(
        private _atmService: AtmService,
        private _router: Router) {
    }

    logout() {
        this._atmService.logout();
        this._router.navigateByUrl('/atm/login');
    }
}
