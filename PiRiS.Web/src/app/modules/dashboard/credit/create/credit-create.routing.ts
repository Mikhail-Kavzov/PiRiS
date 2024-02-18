import { Route } from '@angular/router';
import { CreditCreateComponent } from './credit-create.component';
import { CreditCreateResolver } from './credit-create.resolver';

export const creditCreateRoutes: Route[] = [
    {
        path: '',
        component: CreditCreateComponent,
        resolve: {
            pageData: CreditCreateResolver
        }
    }
];
