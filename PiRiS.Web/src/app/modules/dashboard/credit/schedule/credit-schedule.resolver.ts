import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CreditScheduleDto } from '../../../../../api/api.client';
import { CreditService } from '../credit.service';

@Injectable()
export class CreditScheduleResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _creditService: CreditService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CreditScheduleDto> {
        let creditId = route.queryParams['creditId'];

        return this._creditService.getSchedule(creditId);
    }
}
