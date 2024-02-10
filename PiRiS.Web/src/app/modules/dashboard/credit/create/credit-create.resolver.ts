import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CreditAgreementDto} from '../../../../../api/api.client';
import { CreditService } from '../credit.service';

@Injectable()
export class CreditCreateResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _creditService: CreditService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CreditAgreementDto> {
        return this._creditService.getAgreement();
    }
}
