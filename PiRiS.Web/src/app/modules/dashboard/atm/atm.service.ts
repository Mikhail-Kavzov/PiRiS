import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'api/api.service';
import { AtmLoginDto, TransferMoneyDto, WithdrawMoneyDto } from '../../../../api/api.client';

@Injectable({
    providedIn: 'root'
})
export class AtmService {

    private readonly cardNumberKey = 'cardNumber';

    constructor(
        private _apiService: ApiService,
        private _router: Router) {
    }

    login(creditCardNumber: string, pinCode: string) {
        return this._apiService.apiClient.apiAtmLogin(
            new AtmLoginDto
                ({
                    creditCardNumber: creditCardNumber,
                    creditCardCode: pinCode
                }));
    }

    get cardNumber(): string {
        return localStorage.getItem(this.cardNumberKey) ?? '';
    }

    set cardNumber(creditCardNumber: string) {
        localStorage.setItem(this.cardNumberKey, creditCardNumber);
    }

    logout() {
        localStorage.removeItem(this.cardNumberKey);
    }

    withdrawMoney(cardNumber: string, cardPin: string, sum: number) {
        return this._apiService.apiClient.apiAtmWithdraw(new WithdrawMoneyDto({
            creditCardCode: cardPin,
            creditCardNumber: cardNumber,
            sum: sum
        }));
    }


    getAccount(cardNumber: string, cardPin: string) {
        return this._apiService.apiClient.apiAtmAccount(new AtmLoginDto(
            {
                creditCardNumber: cardNumber,
                creditCardCode: cardPin
            }));
    }

    transferMoney(cardNumber: string, cardPin: string, sum: number, mobilePhone: string, mobilePhoneConfirmation: string) {
        return this._apiService.apiClient.apiAtmTransfer(new TransferMoneyDto({
            creditCardNumber: cardNumber,
            creditCardCode: cardPin,
            sum: sum,
            mobilePhone: mobilePhone,
            mobilePhoneConfirmation: mobilePhoneConfirmation
        }));
    }
}
