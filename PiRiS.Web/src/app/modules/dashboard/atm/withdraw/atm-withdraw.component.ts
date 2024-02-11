import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { AtmReportDto } from '../../../../../api/api.client';
import { Patterns } from '../../../../core/enums/patterns.enum';
import { AtmService } from '../atm.service';

@Component({
    selector: 'atm-withdraw',
    templateUrl: './atm-withdraw.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AtmWithdrawComponent implements OnInit {

    withdrawForm: FormGroup;
    atmReport: AtmReportDto;

    constructor(
        private _formBuilder: FormBuilder,
        private _atmService: AtmService
    ) {
    }

    ngOnInit(): void {
        this.withdrawForm = this._formBuilder.group({
            creditCardCode: ['', [Validators.required, Validators.pattern(Patterns.CreditCardCode)]],
            sum: [0, [Validators.required, Validators.min(0)]]
        });
    }

    withdrawMoney() {

        if (this.withdrawForm.invalid) {
            return;
        }

        let cardNumber = this._atmService.cardNumber;
        let cardPin = this.withdrawForm.get('creditCardCode').value;
        let sum = this.withdrawForm.get('sum').value;

        this._atmService.withdrawMoney(cardNumber, cardPin, sum)
            .subscribe((report) => {
                this.atmReport = report;
            })
    }
}
