import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { DepositDtoPaginationList } from '../../../../../api/api.client';
import { DepositService } from '../deposit.service';

@Injectable()
export class DepositListResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _depositService: DepositService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<DepositDtoPaginationList> {
        return this._depositService.getDeposits(0, 10, '');
    }
}
