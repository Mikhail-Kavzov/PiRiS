import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';
import { AtmReportComponent } from './atm-report.component';
import { atmReportRoutes } from './atm-report.routing';

@NgModule({
    declarations: [
        AtmReportComponent
    ],
    imports: [
        RouterModule.forChild(atmReportRoutes),
        MatButtonModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        SharedModule,
    ],
    exports: [
        AtmReportComponent
    ]
})
export class AtmReportModule {
}
