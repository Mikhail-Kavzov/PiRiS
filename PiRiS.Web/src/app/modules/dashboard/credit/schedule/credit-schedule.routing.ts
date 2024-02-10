import { Route } from '@angular/router';
import { CreditScheduleComponent } from './credit-schedule.component';
import { CreditScheduleResolver } from './credit-schedule.resolver';

export const creditScheduleRoutes: Route[] = [
    {
        path: '',
        component: CreditScheduleComponent,
        resolve: {
            pageData: CreditScheduleResolver
        }
    }
];
