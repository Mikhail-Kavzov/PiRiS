import { Route } from '@angular/router';
import { TokenCorrelationComponent } from './token-correlation.component';
import { TokenCorrelationResolver } from './token-correlation.resolver';

export const tokenCorrelationRoutes: Route[] = [
    {
        path     : '',
        component: TokenCorrelationComponent,
        resolve: {
            pageData: TokenCorrelationResolver
        }
    }
];
