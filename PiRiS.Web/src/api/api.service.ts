/* tslint:disable */
/* eslint-disable */

import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiClient } from './api.client';

@Injectable()
export class ApiService
{
    public apiClient: ApiClient;

    public apiServer: string;

    constructor(private http: HttpClient, @Inject('API_BASE_URL') apiServerUrl: string)
    {
        this.apiClient = new ApiClient(http, apiServerUrl);
        this.apiServer = apiServerUrl;
    }
}
