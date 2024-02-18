import { Route } from '@angular/router';
import { CurrencyResolver } from '../../../../services/currency.resolver';
import { DepositPlanCreateComponent } from './deposit-plan-create.component';

export const depositPlanCreateRoutes: Route[] = [
    {
        path: '',
        component: DepositPlanCreateComponent,
        resolve: [CurrencyResolver]
    }
];
