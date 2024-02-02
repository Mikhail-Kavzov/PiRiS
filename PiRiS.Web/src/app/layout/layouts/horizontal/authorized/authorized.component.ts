import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { Router } from '@angular/router';
import { FuseMediaWatcherService } from '@fuse/services/media-watcher';
import { FuseNavigationService, FuseVerticalNavigationComponent } from '@fuse/components/navigation';
import { Navigation } from 'app/core/navigation/navigation.types';
import { NavigationService } from 'app/core/navigation/navigation.service';
import { AuthService } from 'app/core/auth/auth.service';

@Component({
    selector     : 'authorized-layout',
    templateUrl  : './authorized.component.html',
    styleUrls: ['./authorized.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class AuthorizedLayoutComponent implements OnInit, OnDestroy
{
    isScreenSmall: boolean;
    isScreenMedium: boolean;
    navigation: Navigation;
    isLoadingRedirect: boolean = false;

    get currentYear(): number {
        return new Date().getFullYear();
    }

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _router: Router,
        private _authService: AuthService,
        private _navigationService: NavigationService,
        private _fuseMediaWatcherService: FuseMediaWatcherService,
        private _fuseNavigationService: FuseNavigationService
    )
    { }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    ngOnInit(): void
    {
        this._navigationService.navigation$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((navigation: Navigation) => 
            {
                let currentUrl = this._router.url.replace('/','');
                let navigationNested = JSON.parse(JSON.stringify(navigation));

                if (currentUrl == 'explorer') {
                    var index = navigationNested.horizontal
                        .map(el => el.id)
                        .indexOf('explorer');

                    if (index !== -1) {
                        navigationNested.horizontal.splice(index, 1);
                    }

                    this.navigation = navigationNested;   
                }

                if (currentUrl == '' || !this.isUserLoggedIn()) {
                    var index = navigationNested.horizontal
                        .map(el => el.id)
                        .indexOf('about');

                    if (index !== -1) {
                        navigationNested.horizontal.splice(index, 1);
                    }

                    this.navigation = navigationNested;   
                }
            });
         
        this._fuseMediaWatcherService.onMediaChange$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({matchingAliases}) => 
            {
                this.isScreenMedium = !matchingAliases.includes('lg');
                this.isScreenSmall = !matchingAliases.includes('md');

            });
    }

    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    isNotPage(pageName: string)
    {
        let currentUrl = this._router.url
            .replace('/','');

        return pageName != currentUrl;
    }

    isUserLoggedIn(): boolean
    {
        return this._authService.accessToken && !AuthUtils.isTokenExpired(this._authService.accessToken);
    }

    redirectToExplorer()
    {
        this.isLoadingRedirect = true;

        this._router.navigate(['/explorer'])
            .then(() => {
                this.isLoadingRedirect = false;
            });
    }

    redirectToSignIn()
    {
        this.isLoadingRedirect = true;

        this._router.navigate(['/auth/sign-in'])
            .then(() => {
                this.isLoadingRedirect = false;
            });
    }

    redirectToMain()
    {
        this.isLoadingRedirect = true;

        this._router.navigate(['/main-page-redirect'])
            .then(() => {
                this.isLoadingRedirect = false;
            });
    }

    toggleNavigation(name: string): void
    {
        // Get the navigation
        const navigation = this._fuseNavigationService
            .getComponent<FuseVerticalNavigationComponent>(name);

        if ( navigation )
        {
            // Toggle the opened status
            navigation.toggle();
        }
    }
}
