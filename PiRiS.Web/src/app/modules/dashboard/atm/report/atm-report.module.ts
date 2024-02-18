import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SharedModule } from 'app/shared/shared.module';
import { AtmReportComponent } from './atm-report.component';

@NgModule({
    declarations: [
        AtmReportComponent
    ],
    imports: [
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
