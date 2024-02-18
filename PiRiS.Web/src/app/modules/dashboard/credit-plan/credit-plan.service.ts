import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { Pagination } from '../../../../types/pagination.types';
import { CreditPlanCreateDto, CreditPlanDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class CreditPlanService {

    private _plans: BehaviorSubject<CreditPlanDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<Pagination | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {

    }

    get plans$() {
        return this._plans.asObservable();
    }

    get pagination$() {
        return this._pagination.asObservable();
    }

    createPlan(creditPlan: CreditPlanCreateDto) {
        return this._apiService.apiClient.apiCreditPlanCreate(creditPlan);
    }

    getPlans(page: number, take: number) {
        return this._apiService.apiClient.apiCreditPlanList(page * take, take).pipe(
            tap((plans) => {
                this._plans.next(plans.items);
                this._pagination.next(new Pagination(page, take, plans.totalCount))
            }), catchError((error) => {
                this._plans.next(null)
                this._pagination.next(null);
                return throwError(new Error(error));
            }))
    }

}
