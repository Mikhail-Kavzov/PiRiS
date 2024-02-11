import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { BankService } from './bank.service';

@Component({
    selector: 'bank-page',
    templateUrl: './bank.component.html',
    styleUrls: ['./bank.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class BankComponent {

    constructor(private _router: Router, private _bankService: BankService) {
    }

    closeBankDay() {
        this._bankService.closeBankDay();
    }

    accounts() {
        this._router.navigateByUrl('/account/list');
    }

}
