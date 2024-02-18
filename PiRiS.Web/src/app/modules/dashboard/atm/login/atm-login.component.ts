import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Patterns } from '../../../../core/enums/patterns.enum';
import { AtmService } from '../atm.service';

@Component({
    selector: 'atm-login',
    templateUrl: './atm-login.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class AtmLoginComponent implements OnInit {

    loginForm: FormGroup;

    constructor(
        private _router: Router,
        private _formBuilder: FormBuilder,
        private _atmService: AtmService
    )
    {
    }

    ngOnInit(): void {
        this.loginForm = this._formBuilder.group({
            creditCardNumber: ['', [Validators.required, Validators.pattern(Patterns.CreditCardNumber)]],
            creditCardCode: ['', [Validators.required, Validators.pattern(Patterns.CreditCardCode)]]
        });
    }


    login() {
        if (this.loginForm.invalid) {
            return;
        }

        let cardNumber = this.loginForm.get('creditCardNumber').value;
        let cardPin = this.loginForm.get('creditCardCode').value;

        this._atmService.login(cardNumber, cardPin).subscribe(() => {
            this._atmService.cardNumber = cardNumber;
            this._router.navigateByUrl('/atm/main');
        })
    }
}
