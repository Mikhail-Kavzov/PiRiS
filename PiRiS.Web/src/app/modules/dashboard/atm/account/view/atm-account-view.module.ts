import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SharedModule } from 'app/shared/shared.module';
import { AtmAccountViewComponent } from './atm-account-view.component';

@NgModule({
    declarations: [
        AtmAccountViewComponent
    ],
    imports: [
        MatButtonModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        SharedModule,
    ],
    exports: [
        AtmAccountViewComponent
    ]
})
export class AtmAccountViewModule {
}
