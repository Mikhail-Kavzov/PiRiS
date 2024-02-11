import { Route } from '@angular/router';
import { AccountListComponent } from './account-list.component';
import { AccountListResolver } from './account-list.resolver';

export const accountListRoutes: Route[] = [
    {
        path: '',
        component: AccountListComponent,
        resolve: {
            pageData: AccountListResolver
        }
    }
];
