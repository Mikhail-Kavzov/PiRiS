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
import { AtmLoginComponent } from './atm-login.component';
import { atmLoginRoutes } from './atm-login.routing';

@NgModule({
    declarations: [
        AtmLoginComponent
    ],
    imports: [
        RouterModule.forChild(atmLoginRoutes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        MatSelectModule,
        SharedModule,
    ]
})
export class AtmLoginModule {
}
