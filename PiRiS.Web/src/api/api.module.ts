import { NgModule, ModuleWithProviders } from '@angular/core';
import { CoreModule } from 'app/core/core.module';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from './api.service';

@NgModule({
  imports: [CoreModule, HttpClientModule],
})
export class ApiModule
{
    static forRoot(): ModuleWithProviders<ApiModule>
    {
        return {
            ngModule: ApiModule,
            providers: [ApiService],
        };
    }
}
