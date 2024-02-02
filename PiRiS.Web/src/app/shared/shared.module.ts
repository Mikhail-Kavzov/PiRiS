import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { TruncatePipe } from 'app/core/pipes/truncate.pipe';
import { ChainPipe } from 'app/core/pipes/chaintype.pipe';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { DatePipe } from '@angular/common'

@NgModule({
    declarations: [
        TruncatePipe,
        ChainPipe
    ],
    imports: [
        CommonModule,
        MatIconModule,
        MatInputModule,
        MatButtonModule,
        MatCheckboxModule,
        MatFormFieldModule,
        FormsModule,
        MatDatepickerModule,
        MatNativeDateModule,
        ReactiveFormsModule,
    ],
    providers: [
        DatePipe
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TruncatePipe,
        ChainPipe
    ]
})
export class SharedModule
{
}
