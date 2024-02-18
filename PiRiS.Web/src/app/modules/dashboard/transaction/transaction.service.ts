import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { Pagination } from '../../../../types/pagination.types';
import { TransactionDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class TransactionService {

    private _transactions: BehaviorSubject<TransactionDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<Pagination | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {

    }

    get plans$() {
        return this._transactions.asObservable();
    }

    get pagination$() {
        return this._pagination.asObservable();
    }


    getTransactions(page: number, take: number) {
        return this._apiService.apiClient.apiBankTransactionsList(page * take, take).pipe(
            tap((transactions) => {
                this._transactions.next(transactions.items);
                this._pagination.next(new Pagination(page, take, transactions.totalCount))
            }), catchError((error) => {
                this._transactions.next(null)
                this._pagination.next(null);
                return throwError(new Error(error));
            }))
    }

}
