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
import { CreditCreateComponent } from './credit-create.component';
import { creditCreateRoutes } from './credit-create.routing';
import { CreditCreateResolver } from './credit-create.resolver';

@NgModule({
    declarations: [
        CreditCreateComponent
    ],
    imports: [
        RouterModule.forChild(creditCreateRoutes),
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatProgressSpinnerModule,
        FuseCardModule,
        MatSelectModule,
        SharedModule,
    ],
    providers: [
        CreditCreateResolver
    ]
})
export class CreditCreateModule {
}
