import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { FuseAlertModule } from '@fuse/components/alert';
import { FuseCardModule } from '@fuse/components/card';
import { SharedModule } from 'app/shared/shared.module';
import { AccountListComponent } from './account-list.component';
import { AccountListResolver } from './account-list.resolver';
import { accountListRoutes } from './account-list.routing';

@NgModule({
    declarations: [
        AccountListComponent
    ],
    imports: [
        RouterModule.forChild(accountListRoutes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        FuseAlertModule,
        SharedModule,
        MatPaginatorModule,
        MatProgressBarModule,
        MatRippleModule,
        MatSelectModule,
        MatSlideToggleModule,
        MatTooltipModule,
        MatCheckboxModule,
    ],
    providers: [
        AccountListResolver
    ]
})
export class AccountListModule {
}
