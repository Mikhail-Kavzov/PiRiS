import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { DepositAgreementDto} from '../../../../../api/api.client';
import { DepositService } from '../deposit.service';

@Injectable()
export class DepositCreateResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _depositService: DepositService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<DepositAgreementDto> {
        return this._depositService.getAgreement();
    }
}
