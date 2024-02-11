import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { MainData } from '../../../../types/main-data.model';

@Component({
    selector: 'main-page',
    templateUrl: './main-page.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class MainPageComponent implements OnInit {

    mainButtons: MainData[];

    constructor(private _router: Router) {
    }
    ngOnInit(): void {
        this.mainButtons =
            [
                { link: '/account/list', name: 'Accounts' },
                { link: '/client/list', name: 'Clients' },
                { link: '/bank', name: 'Bank' },
                { link: '/credit/list', name: 'Credits' },
                { link: '/deposit/list', name: 'Deposits' },
                { link: '/credit-plan/list', name: 'Credit Plans' },
                { link: '/deposit-plan/list', name: 'Deposit Plans' },
                { link: '/atm/login', name: 'ATM' },
            ];
    }

    btnClick(btnData: MainData) {
        this._router.navigateByUrl(btnData.link);
    }

}
