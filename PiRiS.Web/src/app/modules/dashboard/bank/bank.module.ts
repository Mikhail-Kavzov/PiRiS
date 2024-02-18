import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { BankComponent } from './bank.component';
import { bankRoutes } from './bank.routing';

@NgModule({
    declarations: [
        BankComponent
    ],
    imports: [
        RouterModule.forChild(bankRoutes),
        MatButtonModule,
        MatIconModule,
        SharedModule,
    ]
})
export class BankModule {
}
