import { Route } from '@angular/router';
import { DepositCreateComponent } from './deposit-create.component';
import { DepositCreateResolver } from './deposit-create.resolver';

export const depositCreateRoutes: Route[] = [
    {
        path: '',
        component: DepositCreateComponent,
        resolve: {
            pageData: DepositCreateResolver
        }
    }
];
