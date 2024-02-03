import { Route } from '@angular/router';
import { ClientResolver } from '../client.resolver';
import { ClientUpdateComponent } from './client-update.component';
import { ClientUpdateResolver } from './client-update.resolver';

export const clientUpdateRoutes: Route[] = [
    {
        path: '',
        component: ClientUpdateComponent,
        resolve: {
            ClientResolver,
            ClientUpdateResolver
        }
    }
];
