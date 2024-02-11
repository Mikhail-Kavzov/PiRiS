import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FuseCardModule } from '@fuse/components/card';
import { SharedModule } from 'app/shared/shared.module';
import { MatSelectModule } from '@angular/material/select';
import { AtmWithdrawComponent } from './atm-withdraw.component';
import { atmWithdrawRoutes } from './atm-withdraw.routing';
import { AtmReportModule } from '../report/atm-report.module';
import { AtmLogoutModule } from '../logout/atm-logout.module';

@NgModule({
    declarations: [
        AtmWithdrawComponent
    ],
    imports: [
        RouterModule.forChild(atmWithdrawRoutes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        AtmReportModule,
        AtmLogoutModule,
        SharedModule,
    ]
})
export class AtmWithdrawModule {
}
