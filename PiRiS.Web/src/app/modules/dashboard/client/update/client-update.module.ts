import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FuseCardModule } from '@fuse/components/card';
import { SharedModule } from 'app/shared/shared.module';
import { ClientUpdateComponent } from './client-update.component';
import { clientUpdateRoutes } from './client-update.routing';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ClientUpdateResolver } from './client-update.resolver';

@NgModule({
    declarations: [
        ClientUpdateComponent
    ],
    providers: [
        ClientUpdateResolver
    ],
    imports: [
        RouterModule.forChild(clientUpdateRoutes),
        MatButtonModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        MatSelectModule,
        MatDatepickerModule,
        SharedModule,
    ]
})
export class ClientUpdateModule {
}
