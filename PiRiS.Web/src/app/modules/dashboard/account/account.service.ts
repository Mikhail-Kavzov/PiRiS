import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { Pagination } from '../../../../types/pagination.types';
import { AccountDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class AccountService {

    private _accounts: BehaviorSubject<AccountDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<Pagination | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {

    }

    get accounts$() {
        return this._accounts.asObservable();
    }

    get pagination$() {
        return this._pagination.asObservable();
    }

    getAccounts(page: number, take: number, accountNumber: string = '') {
        return this._apiService.apiClient.apiAccountList(page, take, accountNumber).pipe(
            tap((accounts) => {

                this._accounts.next(accounts.items);
                this._pagination.next(new Pagination(page, take, accounts.totalCount))

            }), catchError((error) => {

                this._accounts.next(null)
                this._pagination.next(null);

                return throwError(new Error(error));
            }))
    }
}
