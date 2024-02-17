import { Route } from '@angular/router';
import { TransactionListComponent } from './transaction-list.component';
import { TransactionListResolver } from './transaction-list.resolver';

export const transactionListRoutes: Route[] = [
    {
        path: '',
        component: TransactionListComponent,
        resolve: {
            pageData: TransactionListResolver
        }
    }
];
