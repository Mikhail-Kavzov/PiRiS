import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { NavigationData } from '../../../../types/main-data.model';

@Component({
    selector: 'empty-layout',
    templateUrl: './empty.component.html',
    encapsulation: ViewEncapsulation.None
})
export class EmptyLayoutComponent implements OnInit, OnDestroy {
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    isLoadingRedirect: boolean = false;
    mainButtons: NavigationData[];

    constructor(private _router: Router) {
    }
    ngOnInit(): void {
        this.mainButtons =
            [
                { link: '/client/list', name: 'Client List', modulePath: 'client' },
                { link: '/client/create', name: 'Create Client', modulePath: 'client' },
                { link: '/credit/list', name: 'Credit List', modulePath: 'credit' },
                { link: '/credit/create', name: 'Create Credit', modulePath: 'credit' },
                { link: '/deposit/list', name: 'Deposit List', modulePath: 'deposit' },
                { link: '/deposit/create', name: 'Create Deposit', modulePath: 'deposit' },
                { link: '/credit-plan/list', name: 'Plan List', modulePath: 'credit-plan' },
                { link: '/credit-plan/create', name: 'Create Plan', modulePath: 'credit-plan' },
                { link: '/deposit-plan/list', name: 'Plan List', modulePath: 'deposit-plan' },
                { link: '/deposit-plan/create', name: 'Create Plan', modulePath: 'deposit-plan' },
                { link: '/account/list', name: 'Account List', modulePath: 'account' },
                { link: '/bank', name: 'Bank', modulePath: 'bank' },
                { link: '/atm/main', name: 'ATM Start Page', modulePath: 'atm' },
                { link: '/transaction/list', name: 'Transaction List', modulePath: 'transaction' },
            ]
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    isModule(moduleName: string) {
        let modulePath = '/' + moduleName + '/';
        return this._router.url.startsWith(modulePath);
    }

    redirectToPage(page: String) {
        this.isLoadingRedirect = true;

        this._router.navigate([page])
            .then(() => {
                this.isLoadingRedirect = false;
            });
    }
}
