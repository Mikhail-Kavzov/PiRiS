import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { CurrencyDto } from '../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class CurrencyService {

    private _currencies: BehaviorSubject<CurrencyDto[] | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {

    }

    get currencies$() {
        return this._currencies.asObservable();
    }

    getCurrencies() {
        return this._apiService.apiClient.apiCurrencyList().pipe(
            tap((currencies) => {

                this._currencies.next(currencies);
            }), catchError((error) => {
                this._currencies.next(null);
                return throwError(new Error(error));
            }));
    }
}
