import { Route } from '@angular/router';
import { LayoutComponent } from 'app/layout/layout.component';

export const appRoutes: Route[] = [

    { path: 'main-page-redirect', pathMatch: 'full', redirectTo: 'client/list' },

    {
        path: 'client',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'create', loadChildren: () => import('app/modules/dashboard/client/create/client-create.module').then(m => m.ClientCreateModule) },
            { path: 'list', loadChildren: () => import('app/modules/dashboard/client/list/client-list.module').then(m => m.ClientListModule) },
            { path: 'update', loadChildren: () => import('app/modules/dashboard/client/update/client-update.module').then(m => m.ClientUpdateModule) },
        ]
    },
    {
        path: 'deposit-plan',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'create', loadChildren: () => import('app/modules/dashboard/deposit-plan/create/deposit-plan-create.module').then(m => m.DepositPlanCreateModule) },
            { path: 'list', loadChildren: () => import('app/modules/dashboard/deposit-plan/list/deposit-plan-list.module').then(m => m.DepositPlanListModule) },
        ]
    },
    {
        path: 'credit-plan',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'create', loadChildren: () => import('app/modules/dashboard/credit-plan/create/credit-plan-create.module').then(m => m.CreditPlanCreateModule) },
            { path: 'list', loadChildren: () => import('app/modules/dashboard/credit-plan/list/credit-plan-list.module').then(m => m.CreditPlanListModule) },
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
