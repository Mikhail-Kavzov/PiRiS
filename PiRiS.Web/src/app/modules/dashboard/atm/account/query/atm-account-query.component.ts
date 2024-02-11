import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { AccountDto } from '../../../../../../api/api.client';
import { Patterns } from '../../../../../core/enums/patterns.enum';
import { AtmService } from '../../atm.service';

@Component({
    selector: 'atm-account-query',
    templateUrl: './atm-account-query.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AtmAccountQueryComponent implements OnInit {

    accountForm: FormGroup;
    account: AccountDto;

    constructor(
        private _formBuilder: FormBuilder,
        private _atmService: AtmService
    ) {
    }

    ngOnInit(): void {
        this.accountForm = this._formBuilder.group({
            creditCardCode: ['', [Validators.required, Validators.pattern(Patterns.CreditCardCode)]],
        });
    }

    getAccount() {

        if (this.accountForm.invalid) {
            return;
        }

        let cardNumber = this._atmService.cardNumber;
        let cardPin = this.accountForm.get('creditCardCode').value;

        this._atmService.getAccount(cardNumber, cardPin)
            .subscribe((account) => {
                this.account = account;
            })
    }

}
