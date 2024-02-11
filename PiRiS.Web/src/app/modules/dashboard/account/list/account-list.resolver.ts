import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountDtoPaginationList } from '../../../../../api/api.client';
import { AccountService } from '../account.service';

@Injectable()
export class AccountListResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _accountService: AccountService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<AccountDtoPaginationList> {
        return this._accountService.getAccounts(0, 10, '');
    }
}
