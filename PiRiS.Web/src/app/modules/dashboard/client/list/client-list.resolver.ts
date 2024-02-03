import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ClientSortField, ClientViewDtoPaginationList, SortDirection } from '../../../../../api/api.client';
import { ClientService } from '../client.service';

@Injectable({
    providedIn: 'root'
})
export class ClientListResolver implements Resolve<any>
{
    /**
     * Constructor
     */
    constructor(private _clientService: ClientService) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ClientViewDtoPaginationList> {
        return this._clientService.getClients(0, 10, '', SortDirection.ascending, ClientSortField.surname);
    }
}
