import { ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { BankService } from './bank.service';

@Component({
    selector: 'bank-page',
    templateUrl: './bank.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class BankComponent {

    messageText: string = '';
    displayMessage: boolean = false;

    constructor(private _router: Router, private _bankService: BankService, private _changeDetectorRef: ChangeDetectorRef) {
    }

    closeBankDay() {
        this._bankService.closeBankDay()
            .subscribe(() => {
                this.showSuccess('Day was closed');
            },
                () => {
                    this.showError();
                });
    }

    accounts() {
        this._router.navigateByUrl('/account/list');
    }

    hideMessage() {
        this._changeDetectorRef.markForCheck();
        setTimeout(() => {
            this.displayMessage = false;
            this._changeDetectorRef.markForCheck();
        }, 3000);
    }

    showSuccess(text: string) {
        this.messageText = text;
        this.displayMessage = true;
        this.hideMessage();
    }

    showError() {
        this.messageText = 'Error occured';
        this.displayMessage = true;
        this.hideMessage();
    }

}
