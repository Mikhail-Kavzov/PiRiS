import { Route } from '@angular/router';
import { CurrencyResolver } from '../../../../services/currency.resolver';
import { CreditPlanCreateComponent } from './credit-plan-create.component';

export const creditPlanCreateRoutes: Route[] = [
    {
        path: '',
        component: CreditPlanCreateComponent,
        resolve: [CurrencyResolver]

    }
];
