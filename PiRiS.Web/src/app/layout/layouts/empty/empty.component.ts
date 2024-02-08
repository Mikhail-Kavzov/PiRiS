import { Component, OnDestroy, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';

@Component({
    selector     : 'empty-layout',
    templateUrl  : './empty.component.html',
    encapsulation: ViewEncapsulation.None
})
export class EmptyLayoutComponent implements OnDestroy
{
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    isLoadingRedirect: boolean = false;
    /**
     * Constructor
     */
    constructor(private _router: Router)
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    isNotPage(pageName: string) {
        let currentUrl = this._router.url
            .replace('/', '');

        return pageName != currentUrl;
    }

    redirectToPage(page: String) {
        this.isLoadingRedirect = true;

        this._router.navigate([page])
            .then(() => {
                this.isLoadingRedirect = false;
            });
    }
}
