import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { Pagination } from '../../../../types/pagination.types';
import { CurrencyDto, DepositCreateDto, DepositDto, DepositPlanAgreementDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class DepositService {

    private _deposits: BehaviorSubject<DepositDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<Pagination | null> = new BehaviorSubject(null);
    private _plans: BehaviorSubject<DepositPlanAgreementDto[] | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {

    }

    get plans$() {
        return this._plans.asObservable();
    }

    get deposits$() {
        return this._deposits.asObservable();
    }

    get pagination$() {
        return this._pagination.asObservable();
    }

    createDeposit(depositCreateDto: DepositCreateDto) {
        return this._apiService.apiClient.apiDepositCreate(depositCreateDto);
    }

    getDeposits(page: number, take: number, depositNumber: string = '') {
        return this._apiService.apiClient.apiDepositList(page, take, depositNumber).pipe(
            tap((deposits) => {

                this._deposits.next(deposits.items);
                this._pagination.next(new Pagination(page, take, deposits.totalCount))

            }), catchError((error) => {

                this._deposits.next(null)
                this._pagination.next(null);

                return throwError(new Error(error));
            }))
    }

    getAgreement() {
        return this._apiService.apiClient.apiDepositAgreement().pipe(
            tap((agreement) => {
                this._plans.next(agreement.depositPlans);

            }), catchError((error) => {
                this._plans.next(null);
                return throwError(new Error(error));
            }));
    }


    closeDeposit(depositId: number) {
        return this._apiService.apiClient.apiDepositClose(depositId);
    }

    withdrawPercents(depositId: number) {
        return this._apiService.apiClient.apiDepositWithdraw(depositId);
    }
}
