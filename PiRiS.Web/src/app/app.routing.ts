import { Route } from '@angular/router';
import { LayoutComponent } from 'app/layout/layout.component';

export const appRoutes: Route[] = [

    { path: 'main-page-redirect', pathMatch: 'full', redirectTo: '' },
    {
        path: 'client',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'create', loadChildren: () => import('app/modules/dashboard/client/create/client-create.module').then(m => m.ClientCreateModule) },
            { path: 'list', loadChildren: () => import('app/modules/dashboard/client/list/client-list.module').then(m => m.ClientListModule) },
        ]
    },


    {
        path: 'maintenance',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'error-404', loadChildren: () => import('app/modules/maintenance/error-404/error-404.module').then(m => m.Error404Module) },
        ]
    },

    // Always last in order
    { path: '**', redirectTo: 'maintenance/error-404' }
];
