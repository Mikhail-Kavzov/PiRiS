import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { FuseAlertModule } from '@fuse/components/alert';
import { FuseCardModule } from '@fuse/components/card';
import { SharedModule } from 'app/shared/shared.module';
import { TransactionListComponent } from './transaction-list.component';
import { TransactionListResolver } from './transaction-list.resolver';
import { transactionListRoutes } from './transaction-list.routing';

@NgModule({
    declarations: [
        TransactionListComponent
    ],
    imports: [
        RouterModule.forChild(transactionListRoutes),
        MatButtonModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        FuseAlertModule,
        SharedModule,
        MatPaginatorModule,
        MatProgressBarModule,
        MatRippleModule,
        MatSlideToggleModule,
        MatTooltipModule,
    ],
    providers: [
        TransactionListResolver
    ]
})
export class TransactionListModule {
}
