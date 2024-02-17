import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CurrencyDto } from '../../api/api.client';
import { CurrencyService } from './currency.service';

@Injectable({
    providedIn: 'root'
})
export class CurrencyResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _currencyService: CurrencyService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CurrencyDto[]> {
        return this._currencyService.getCurrencies();
    }
}
