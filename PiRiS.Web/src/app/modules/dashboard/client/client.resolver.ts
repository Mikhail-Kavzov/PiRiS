import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ClientAdditionalsDto } from '../../../../api/api.client';
import { ClientService } from './client.service';

@Injectable({
    providedIn: 'root'
})
export class ClientResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _clientService: ClientService) {
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
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ClientAdditionalsDto> {
        return this._clientService.getAdditionals();
    }
}
