import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, catchError, Observable, of, switchMap, tap } from 'rxjs';
import { ApiService } from 'api/api.service';
import { User } from 'app/core/user/user.types';

@Injectable({
    providedIn: 'root'
})
export class UserService
{
    private _user: BehaviorSubject<User> = new BehaviorSubject<User>(null);

    /**
     * Constructor
     */
    constructor(private _apiService: ApiService)
    {              
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Setter & getter for user
     *
     * @param value
     */
    set user(value: User)
    {
        localStorage.setItem(
            'userInfo', 
            JSON.stringify(value));

        this._user.next(value);
    }

    get user(): User 
    {
        return JSON.parse(localStorage.getItem('userInfo')) as User;     
    }

    get user$(): Observable<User>
    {
        return this._user.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    get(): Observable<User>
    { 
        return this._apiService.apiClient.getUserInfo().pipe(
            tap((user: User) => 
            {
                this.user = user;
            }),
            catchError(() => { 
                return of(null); 
            })
        );
    }
}
