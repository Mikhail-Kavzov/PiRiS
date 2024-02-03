import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of, switchMap, take, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { ClientAdditionalsDto, ClientDto, ClientPaginationDto, ClientSortField, ClientViewDto, SortDirection } from '../../../../api/api.client';
import { Pagination } from '../../../../types/pagination.types';

@Injectable({
    providedIn: 'root'
})
export class ClientService {

    private _clientAdditionals: BehaviorSubject<ClientAdditionalsDto | null> = new BehaviorSubject(null);

    private _clients: BehaviorSubject<ClientViewDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<Pagination | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {
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


    getClients(page: number = 0, take: number = 0, surname: string = '',
        sortDirection: SortDirection = SortDirection.ascending, sortField: ClientSortField.surname){
        return this._apiService.apiClient.list(new ClientPaginationDto({
            skip: page * take,
            take: take,
            surname: surname,
            sortDirection: sortDirection,
            sortField: sortField
        })).pipe(
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
        return this._apiService.apiClient.additionals().pipe(
            tap((result) => {
                this._clientAdditionals.next(result);

            }),
            catchError((error) => {

                this._clientAdditionals.next(null);
                return throwError(new Error(error));
            }));
    }

    createClient(clientDto: ClientDto) {
        return this._apiService.apiClient.create(clientDto);     
    }

    deleteClient(id: number) {
        return this._apiService.apiClient.delete(id).pipe(
            catchError((error) => throwError(new Error(error))
            ));
    }

}
