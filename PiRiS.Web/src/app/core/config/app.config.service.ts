import { Injectable } from '@angular/core';
import { environment } from 'env/environment';
import { AppServerConfig } from 'app/core/config/app.config.server';

@Injectable()
export class AppConfigService 
{
    public apiServer = '';

    constructor() 
    {
        if (environment.published) 
        {
            const apiServerConfig: AppServerConfig = 
                (window as any).appConfig;

            this.apiServer = apiServerConfig.apiServer;
        } 
        else 
        {
            this.apiServer = environment.apiServer;
        }
    }
}