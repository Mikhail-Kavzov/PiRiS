import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { Pagination } from '../../../../types/pagination.types';
import { DepositPlanCreateDto, DepositPlanDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class DepositPlanService {

    private _plans: BehaviorSubject<DepositPlanDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<Pagination | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {

    }

    get plans$() {
        return this._plans.asObservable();
    }

    get pagination$() {
        return this._pagination.asObservable();
    }

    createPlan(depositPlan: DepositPlanCreateDto) {
        return this._apiService.apiClient.apiDepositPlanCreate(depositPlan);
    }

    getPlans(page: number, take: number) {
        return this._apiService.apiClient.apiDepositPlanList(page, take).pipe(
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
