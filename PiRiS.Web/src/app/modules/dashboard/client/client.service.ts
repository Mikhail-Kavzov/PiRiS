import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of, switchMap, take, tap, throwError } from 'rxjs';
import { ApiService } from 'api/api.service';
import { ClientAdditionalsDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class ClientService {

    private _clientAdditionals: BehaviorSubject<ClientAdditionalsDto | null> = new BehaviorSubject(null);

    constructor(private _apiService: ApiService) {
    }

    getAdditionals() {
        return this._apiService.apiClient.additionals().pipe(
            map((result) => {
                this._clientAdditionals.next(result);
            }),
            catchError((error) => throwError(error)));
    } 

}
