import { Component, Input, ViewEncapsulation } from '@angular/core';
import { AccountDto } from '../../../../../../api/api.client';

@Component({
    selector: 'atm-account-view',
    templateUrl: './atm-account-view.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class AtmAccountViewComponent{

    @Input()
    account: AccountDto;

}
