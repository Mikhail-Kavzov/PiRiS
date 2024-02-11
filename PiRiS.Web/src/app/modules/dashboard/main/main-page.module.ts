import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'app/shared/shared.module';
import { MainPageComponent } from './main-page.component';
import { mainPageRoutes } from './main-page.routing';

@NgModule({
    declarations: [
        MainPageComponent
    ],
    imports: [
        RouterModule.forChild(mainPageRoutes),
        MatButtonModule,
        MatIconModule,
        SharedModule,
    ]
})
export class MainPageModule {
}
