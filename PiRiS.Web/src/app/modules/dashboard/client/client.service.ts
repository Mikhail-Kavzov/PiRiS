import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { ClientAdditionalsDto, ClientDto, ClientSortField, ClientViewDto, SortDirection } from '../../../../api/api.client';
import { Pagination } from '../../../../types/pagination.types';

@Injectable({
    providedIn: 'root'
})
export class ClientService {

    private _clientAdditionals: BehaviorSubject<ClientAdditionalsDto | null> = new BehaviorSubject(null);

    private _clients: BehaviorSubject<ClientViewDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<Pagination | null> = new BehaviorSubject(null);
    private _client: BehaviorSubject<ClientDto | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {
    }

    get client$() {
        return this._client.asObservable();
    }

    get pagination$() {
        return this._pagination.asObservable();
    }

    get clients$() {
        return this._clients.asObservable();
    }

    get additionals$() {
        return this._clientAdditionals.asObservable();
    }

    getClient(id: number) {
        return this._apiService.apiClient.apiClientClient(id).pipe(
            tap((client: ClientDto) => {
                this._client.next(client);
            }),
            catchError((error) => {
                this._client.next(null);
                return throwError(new Error(error));
            })
        )
    }

    getClients(page: number = 0, take: number = 0, surname: string = '',
        sortDirection: SortDirection = SortDirection.ascending, sortField: ClientSortField.surname) {
        return this._apiService.apiClient.apiClientList(page * take, take, surname, sortField, sortDirection).pipe(
            tap((result) => {
                this._clients.next(result.items);
                this._pagination.next(new Pagination(page, take, result.totalCount));
            }),
            catchError((error) => {
                this._pagination.next(null);
                this._clients.next(null);
                return throwError(new Error(error));
            })
        )
    }


    getAdditionals(): Observable<ClientAdditionalsDto> {
        return this._apiService.apiClient.apiClientAdditionals().pipe(
            tap((result) => {
                this._clientAdditionals.next(result);
            }),
            catchError((error) => {

                this._clientAdditionals.next(null);
                return throwError(new Error(error));
            }));
    }

    updateClient(clientDto: ClientDto) {
        return this._apiService.apiClient.apiClientUpdate(clientDto);
    }

    createClient(clientDto: ClientDto) {
        return this._apiService.apiClient.apiClientCreate(clientDto);
    }

    deleteClient(id: number) {
        return this._apiService.apiClient.apiClientDelete(id);
    }

}
