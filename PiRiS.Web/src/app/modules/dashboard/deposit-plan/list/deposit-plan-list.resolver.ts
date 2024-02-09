import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { DepositPlanDtoPaginationList} from '../../../../../api/api.client';
import { DepositPlanService } from '../deposit-plan.service';

@Injectable()
export class DepositPlanListResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _depositPlanService: DepositPlanService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<DepositPlanDtoPaginationList> {
        return this._depositPlanService.getPlans(0, 10);
    }
}
