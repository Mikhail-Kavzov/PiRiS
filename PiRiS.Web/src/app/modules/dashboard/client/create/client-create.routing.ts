import { Route } from '@angular/router';
import { ClientResolver } from '../client.resolver';
import { ClientCreateComponent } from './client-create.component';

export const clientCreateRoutes: Route[] = [
    {
        path: '',
        component: ClientCreateComponent,
        resolve: {
            pageData: ClientResolver
        }
    }
];
