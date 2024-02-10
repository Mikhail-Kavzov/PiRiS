import { Route } from '@angular/router';
import { CreditListComponent } from './credit-list.component';
import { CreditListResolver } from './credit-list.resolver';

export const creditListRoutes: Route[] = [
    {
        path: '',
        component: CreditListComponent,
        resolve: {
            pageData: CreditListResolver
        }
    }
];
