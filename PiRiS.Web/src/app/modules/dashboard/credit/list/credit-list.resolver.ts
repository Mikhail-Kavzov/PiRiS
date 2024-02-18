import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CreditDtoPaginationList } from '../../../../../api/api.client';
import { CreditService } from '../credit.service';

@Injectable()
export class CreditListResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _creditService: CreditService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CreditDtoPaginationList> {
        return this._creditService.getCredits(0, 10, '');
    }
}
