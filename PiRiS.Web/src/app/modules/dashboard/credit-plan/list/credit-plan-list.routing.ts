import { Route } from '@angular/router';
import { CreditPlanListComponent } from './credit-plan-list.component';
import { CreditPlanListResolver } from './credit-plan-list.resolver';

export const creditPlanListRoutes: Route[] = [
    {
        path: '',
        component: CreditPlanListComponent,
        resolve: {
            pageData: CreditPlanListResolver
        }
    }
];
