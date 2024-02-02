import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of, switchMap, take, tap, throwError } from 'rxjs';
import { TokenCorrelationDto, TokenPaginationDto, UpdateTokenCorrelationDto } from '../../../../api/api.client';
import { ApiService } from 'api/api.service';
import { TokenPagination } from './token-correlation.types';

@Injectable({
    providedIn: 'root'
})
export class TokenCorrelationService {
    // Private
    private _token: BehaviorSubject<TokenCorrelationDto | null> = new BehaviorSubject(null);
    private _tokens: BehaviorSubject<TokenCorrelationDto[] | null> = new BehaviorSubject(null);
    private _pagination: BehaviorSubject<TokenPagination | null> = new BehaviorSubject(null);

    /**
     * Constructor
     */
    constructor(private _apiService: ApiService) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for token
     */
    get token$(): Observable<TokenCorrelationDto> {
        return this._token.asObservable();
    }

    /**
     * Getter for tokens
     */
    get tokens$(): Observable<TokenCorrelationDto[]> {
        return this._tokens.asObservable();
    }

    get pagination$(): Observable<TokenPagination> {
        return this._pagination.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get tokens
     *
     *
     * @param page
     * @param size
     * @param sort
     * @param order
     * @param search
     */
    getTokens(page: number = 0, size: number = 10, search: string = '', isNull: boolean = true):
        Observable<TokenCorrelationDto[]> {

        return this._apiService.apiClient.getTokensCorrelation(new TokenPaginationDto(
            {
                first: size,
                offset: page * size,
                isNull: isNull,
                symbol: search
            }))
            .pipe(
                tap((pagedList) => {
                    this._tokens.next(pagedList.items);
                    this._pagination.next(new TokenPagination(page, size, pagedList.totalCount));
                }),
                catchError(
                    (error): Observable<any> => {
                        this._tokens.next(null);
                        this._pagination.next(null);

                        return throwError(
                            () => new Error(error));
                    },
                ),
            );
    }

    getTokenById(id: string): Observable<TokenCorrelationDto> {
        return this._tokens.pipe(
            take(1),
            map((tokens) => {

                // Find the token
                const token = tokens.find(item => item.id === id) || null;
                // Update the token
                this._tokens.next(tokens);

                // Return the token
                return token;
            }),
            switchMap((token) => {

                if (!token) {
                    return throwError('Could not found token with id of ' + id + '!');
                }
                return of(token);
            })
        );
    }



    /**
     * Update token
     *
     * @param id
     * @param token
     */
    updateToken(token: TokenCorrelationDto): Observable<boolean> {

        let updateTokenDto: UpdateTokenCorrelationDto = {
            id: token.id,
            coinGeckoApiId: token.coinGeckoApiId,
            init: token.init,
            toJSON: token.toJSON
        };
        return this._apiService.apiClient.updateTokenCorrelation(updateTokenDto)
            .pipe(
                tap((updated) => {
                    let tokens = this._tokens.value;
                    let index = tokens.findIndex(item => item.id === token.id);
                    tokens[index] = token;
                    this._tokens.next(tokens);

                    return updated;
                }),
                catchError(
                    (error): Observable<any> => {

                        return throwError(
                            () => new Error(error));
                    },
                ),
            );
    }
}
