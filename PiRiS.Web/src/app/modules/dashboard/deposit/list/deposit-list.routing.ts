import { Route } from '@angular/router';
import { DepositListComponent } from './deposit-list.component';
import { DepositListResolver } from './deposit-list.resolver';

export const depositListRoutes: Route[] = [
    {
        path: '',
        component: DepositListComponent,
        resolve: {
            pageData: DepositListResolver
        }
    }
];
