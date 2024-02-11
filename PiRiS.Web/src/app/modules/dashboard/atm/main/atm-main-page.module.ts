import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { AtmLogoutModule } from '../logout/atm-logout.module';
import { atmMainPageRoutes } from './atm-main-page.routing';
import { AtmMainPageComponent } from './atm-main-page.component';

@NgModule({
    declarations: [
        AtmMainPageComponent
    ],
    imports: [
        RouterModule.forChild(atmMainPageRoutes),
        MatButtonModule,
        MatIconModule,
        AtmLogoutModule,
        SharedModule,
    ]
})
export class AtmMainPageModule {
}
