import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ClientDto } from '../../../../../api/api.client';
import { ClientService } from '../client.service';

@Injectable()
export class ClientUpdateResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _clientService: ClientService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ClientDto> {
        let clientId = route.queryParams['clientId'];

        return this._clientService.getClient(clientId);
    }
}
