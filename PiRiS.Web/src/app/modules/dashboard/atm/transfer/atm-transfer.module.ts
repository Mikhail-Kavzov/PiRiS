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
import { AtmTransferComponent } from './atm-transfer.component';
import { atmTransferRoutes } from './atm-transfer.routing';
import { AtmReportModule } from '../report/atm-report.module';
import { AtmLogoutModule } from '../logout/atm-logout.module';

@NgModule({
    declarations: [
        AtmTransferComponent
    ],
    imports: [
        RouterModule.forChild(atmTransferRoutes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        MatSelectModule,
        AtmReportModule,
        AtmLogoutModule,
        SharedModule,
    ]
})
export class AtmTransferModule {
}
