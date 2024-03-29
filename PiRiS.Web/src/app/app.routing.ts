import { Route } from '@angular/router';
import { LayoutComponent } from 'app/layout/layout.component';

export const appRoutes: Route[] = [

    { path: 'main-page-redirect', pathMatch: 'full', redirectTo: '' },

    {
        path: '',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: '', loadChildren: () => import('app/modules/dashboard/main/main-page.module').then(m => m.MainPageModule) },
        ]
    },
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
        path: 'transaction',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'list', loadChildren: () => import('app/modules/dashboard/transaction/list/transaction-list.module').then(m => m.TransactionListModule) },
        ]
    },
    {
        path: 'atm',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'account-query', loadChildren: () => import('app/modules/dashboard/atm/account/query/atm-account-query.module').then(m => m.AtmAccountQueryModule) },
            { path: 'login', loadChildren: () => import('app/modules/dashboard/atm/login/atm-login.module').then(m => m.AtmLoginModule) },
            { path: 'main', loadChildren: () => import('app/modules/dashboard/atm/main/atm-main-page.module').then(m => m.AtmMainPageModule) },
            { path: 'transfer', loadChildren: () => import('app/modules/dashboard/atm/transfer/atm-transfer.module').then(m => m.AtmTransferModule )},
            { path: 'withdraw', loadChildren: () => import('app/modules/dashboard/atm/withdraw/atm-withdraw.module').then(m => m.AtmWithdrawModule) },

        ]
    },
    {
        path: 'deposit',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'create', loadChildren: () => import('app/modules/dashboard/deposit/create/deposit-create.module').then(m => m.DepositCreateModule) },
            { path: 'list', loadChildren: () => import('app/modules/dashboard/deposit/list/deposit-list.module').then(m => m.DepositListModule) },
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
        path: 'credit',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'create', loadChildren: () => import('app/modules/dashboard/credit/create/credit-create.module').then(m => m.CreditCreateModule) },
            { path: 'list', loadChildren: () => import('app/modules/dashboard/credit/list/credit-list.module').then(m => m.CreditListModule) },
            { path: 'schedule', loadChildren: () => import('app/modules/dashboard/credit/schedule/credit-schedule.module').then(m => m.CreditScheduleModule) },
        ]
    },

    {
        path: 'account',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'list', loadChildren: () => import('app/modules/dashboard/account/list/account-list.module').then(m => m.AccountListModule) },
        ]
    },
    {
        path: 'bank',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: '', loadChildren: () => import('app/modules/dashboard/bank/bank.module').then(m => m.BankModule) },
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
