import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { MainData } from '../../../../../types/main-data.model';

@Component({
    selector: 'atm-main-page',
    templateUrl: './atm-main-page.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AtmMainPageComponent implements OnInit {

    mainButtons: MainData[];

    constructor(private _router: Router) {
    }
    ngOnInit(): void {
        this.mainButtons =
            [
                { link: '/atm/withdraw', name: 'Withdraw Money' },
                { link: '/atm/account-query', name: 'Show Account' },
                { link: '/atm/transfer', name: 'Transfer Money' }
            ];
    }

    btnClick(btnData: MainData) {
        this._router.navigateByUrl(btnData.link);
    }

}
