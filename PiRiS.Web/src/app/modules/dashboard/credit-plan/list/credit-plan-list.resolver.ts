import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CreditPlanDtoPaginationList} from '../../../../../api/api.client';
import { CreditPlanService } from '../credit-plan.service';

@Injectable()
export class CreditPlanListResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _creditPlanService: CreditPlanService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CreditPlanDtoPaginationList> {
        return this._creditPlanService.getPlans(0, 10);
    }
}
