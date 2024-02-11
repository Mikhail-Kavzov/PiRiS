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
import { atmAccountQueryRoutes } from './atm-account-query.routing';
import { AtmAccountQueryComponent } from './atm-account-query.component';
import { AtmLogoutModule } from '../../logout/atm-logout.module';
import { AtmAccountViewModule } from '../view/atm-account-view.module';

@NgModule({
    declarations: [
        AtmAccountQueryComponent
    ],
    imports: [
        RouterModule.forChild(atmAccountQueryRoutes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        AtmAccountViewModule,
        AtmLogoutModule,
        SharedModule,
    ]
})
export class AtmAccountQueryModule {
}
