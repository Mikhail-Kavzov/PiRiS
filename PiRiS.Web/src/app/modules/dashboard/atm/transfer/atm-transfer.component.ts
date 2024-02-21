import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { FuseValidators } from '../../../../../@fuse/validators';
import { AtmReportDto } from '../../../../../api/api.client';
import { Patterns } from '../../../../core/enums/patterns.enum';
import { AtmService } from '../atm.service';

@Component({
    selector: 'atm-transfer',
    templateUrl: './atm-transfer.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AtmTransferComponent implements OnInit {

    transferForm: FormGroup;
    atmReport: AtmReportDto;

    constructor(
        private _formBuilder: FormBuilder,
        private _atmService: AtmService
    ) {
    }

    ngOnInit(): void {
        this.transferForm = this._formBuilder.group({
            creditCardCode: ['', [Validators.required, Validators.pattern(Patterns.CreditCardCode)]],
            sum: [0, [Validators.required, Validators.min(0)]],
            mobilePhone: ['', [Validators.required, Validators.pattern(Patterns.Phone)]],
            mobilePhoneConfirmation: ['', [Validators.required, Validators.pattern(Patterns.Phone)]],
            operatorId: ['0', Validators.required]
        },
        {
            validators: FuseValidators.mustMatch('mobilePhone','mobilePhoneConfirmation')
        });
    }

    transferMoney() {

        if (this.transferForm.invalid) {
            return;
        }

        let cardNumber = this._atmService.cardNumber;
        let cardPin = this.transferForm.get('creditCardCode').value;
        let sum = this.transferForm.get('sum').value;
        let mobilePhone = this.transferForm.get('mobilePhone').value;

        this._atmService.transferMoney(cardNumber, cardPin, sum, mobilePhone, mobilePhone)
            .subscribe((report) => {
                this.atmReport = report;
                console.log(JSON.stringify(report));
            })
    }
}
