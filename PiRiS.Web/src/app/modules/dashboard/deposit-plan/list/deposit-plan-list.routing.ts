import { Route } from '@angular/router';
import { DepositPlanListComponent } from './deposit-plan-list.component';
import { DepositPlanListResolver } from './deposit-plan-list.resolver';

export const depositPlanListRoutes: Route[] = [
    {
        path: '',
        component: DepositPlanListComponent,
        resolve: {
            pageData: DepositPlanListResolver
        }
    }
];
