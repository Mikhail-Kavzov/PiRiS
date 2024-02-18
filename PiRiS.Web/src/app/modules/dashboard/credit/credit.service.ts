import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { Pagination } from '../../../../types/pagination.types';
import { CreditCreateDto, CreditDto, CreditPlanAgreementDto, CreditScheduleDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class CreditService {

    private _credits: BehaviorSubject<CreditDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<Pagination | null> = new BehaviorSubject(null);
    private _plans: BehaviorSubject<CreditPlanAgreementDto[] | null> = new BehaviorSubject(null);
    private _schedule: BehaviorSubject<CreditScheduleDto | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {

    }

    get plans$() {
        return this._plans.asObservable();
    }

    get schedule$() {
        return this._schedule.asObservable();
    }

    get credits$() {
        return this._credits.asObservable();
    }

    get pagination$() {
        return this._pagination.asObservable();
    }

    createCredit(creditCreateDto: CreditCreateDto) {
        return this._apiService.apiClient.apiCreditCreate(creditCreateDto);
    }

    getCredits(page: number, take: number, creditNumber: string = '') {
        return this._apiService.apiClient.apiCreditList(page * take, take, creditNumber).pipe(
            tap((credits) => {

                this._credits.next(credits.items);
                this._pagination.next(new Pagination(page, take, credits.totalCount))

            }), catchError((error) => {

                this._credits.next(null)
                this._pagination.next(null);

                return throwError(new Error(error));
            }))
    }

    getAgreement() {
        return this._apiService.apiClient.apiCreditAgreement().pipe(
            tap((agreement) => {
                this._plans.next(agreement.creditPlans);

            }), catchError((error) => {
                this._plans.next(null);
                return throwError(new Error(error));
            }));
    }


    closeCredit(creditId: number) {
        return this._apiService.apiClient.apiCreditClose(creditId);
    }

    payPercents(creditId: number) {
        return this._apiService.apiClient.apiCreditPay(creditId);
    }

    getSchedule(creditId: number) {
        return this._apiService.apiClient.apiCreditSchedule(creditId).pipe(
            tap((schedule) => {
                this._schedule.next(schedule);

            }), catchError((error) => {

                this._schedule.next(null);

                return throwError(new Error(error));
            }));
    }
}
