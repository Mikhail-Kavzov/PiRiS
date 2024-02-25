import { Component, Input, ViewEncapsulation } from '@angular/core';
import { AccountDto } from '../../../../../../api/api.client';
import { PrintService } from 'app/modules/dashboard/print/print.service';

@Component({
    selector: 'atm-account-view',
    templateUrl: './atm-account-view.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class AtmAccountViewComponent{

    @Input()
    account: AccountDto;

    constructor(private _printService: PrintService) {

    }
    print(){

        let debit = this.account.debit.toFixed(2);
        let credit = this.account.credit.toFixed(2);
        let balance = this.account.balance.toFixed(2);

        let content = `Account Number: ${this.account.accountNumber}\n Plan Name: ${this.account.accountPlanName} \nCode: ${this.account.accountPlanCode}\n
        Debit: ${debit} BYN\n Credit: ${credit} BYN\n Balance: ${balance} BYN\n Type: ${this.account.accountPlanType}`;
        this._printService.print(content);
    }

}
