import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { AtmLogoutComponent } from './atm-logout.component';

@NgModule({
    declarations: [
        AtmLogoutComponent
    ],
    imports: [
        MatButtonModule,
        MatIconModule,
        SharedModule,
    ],
    exports: [
        AtmLogoutComponent
    ]
})
export class AtmLogoutModule {
}
