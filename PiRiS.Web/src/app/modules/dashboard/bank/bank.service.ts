import { Injectable } from '@angular/core';
import { ApiService } from 'api/api.service';

@Injectable({
    providedIn: 'root'
})
export class BankService {


    constructor(private _apiService: ApiService) {
    }

    closeBankDay() {
        return this._apiService.apiClient.apiBankCloseDay();
    }
}
