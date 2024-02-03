import { Route } from '@angular/router';
import { ClientListComponent } from './client-list.component';
import { ClientListResolver } from './client-list.resolver';

export const clientListRoutes: Route[] = [
    {
        path: '',
        component: ClientListComponent,
        resolve: {
            pageData: ClientListResolver
        }
    }
];
