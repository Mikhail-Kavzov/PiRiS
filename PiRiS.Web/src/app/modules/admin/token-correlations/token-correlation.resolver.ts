import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve,RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenCorrelationService } from './token-correlation.service';
import { TokenCorrelationDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class TokenCorrelationResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _tokenService: TokenCorrelationService) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Resolver
     *
     * @param route
     * @param state
     */
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<TokenCorrelationDto[]> {
        return this._tokenService.getTokens();
    }
}
