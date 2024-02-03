import { Route } from '@angular/router';
import { LayoutComponent } from 'app/layout/layout.component';

// @formatter:off
/* eslint-disable max-len */
/* eslint-disable @typescript-eslint/explicit-function-return-type */
export const appRoutes: Route[] = [


    // Routes for guests
    // Auth maintenance routes
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
    // Maintenance routes
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
