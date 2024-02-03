import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExtraOptions, PreloadAllModules, RouterModule } from '@angular/router';
import { FuseModule } from '@fuse';
import { FuseConfigModule } from '@fuse/services/config';
import { CoreModule } from 'app/core/core.module';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ApiModule } from 'api/api.module';
import { AppConfigService } from 'app/core/config/app.config.service';
import { appConfig } from 'app/core/config/app.config';
import { LayoutModule } from 'app/layout/layout.module';
import { AppComponent } from 'app/app.component';
import { ErrorInterceptor } from 'app/core/error/error.interceptor';
import { MessagesService } from 'app/core/error/error.messages';
import { ErrorsService } from 'app/core/error/error.service';
import { appRoutes } from 'app/app.routing';

const routerConfig: ExtraOptions = {
    preloadingStrategy: PreloadAllModules,
    scrollPositionRestoration: 'enabled'
};

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        RouterModule.forRoot(appRoutes, routerConfig),

        // Fuse, FuseConfig
        FuseModule,
        FuseConfigModule.forRoot(appConfig),

        // Angular material
        MatSnackBarModule,

        // Core module of your application
        CoreModule,

        // Layout module of your application
        LayoutModule,

        // API module of your application
        ApiModule.forRoot(),
    ],
    providers: [
        AppConfigService,
        MessagesService,
        ErrorsService,
        {
            provide: 'API_BASE_URL',
            deps: [AppConfigService],
            useFactory: (config: AppConfigService)
                : string => config.apiServer,
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ErrorInterceptor,
            multi: true
        },
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {
}
