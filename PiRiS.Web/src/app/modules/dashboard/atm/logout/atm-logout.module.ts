import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { AtmLogoutComponent } from './atm-logout.component';
import { atmLogoutRoutes } from './atm-logout.routing';

@NgModule({
    declarations: [
        AtmLogoutComponent
    ],
    imports: [
        RouterModule.forChild(atmLogoutRoutes),
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
