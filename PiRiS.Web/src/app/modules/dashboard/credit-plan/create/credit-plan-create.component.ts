import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Subject } from 'rxjs';
import { CreditPlanCreateDto, CurrencyDto } from '../../../../../api/api.client';
import { CreditPlanService } from '../credit-plan.service';

@Component({
    selector: 'credit-plan-create',
    templateUrl: './credit-plan-create.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class CreditPlanCreateComponent implements OnInit, OnDestroy {
    creditForm: FormGroup;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    currencies: CurrencyDto[];

    constructor(
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _creditPlanService: CreditPlanService
    ) {
    }

    ngOnInit(): void {

        this.creditForm = this._formBuilder.group({
            monthPeriod: [1, [Validators.required, Validators.min(1)]],
            name: ['', Validators.required],
            creditType: [0, Validators.required],
            percent: [1, [Validators.required, Validators.min(0)]],
            currencyId: [1, Validators.required],
        })


    }
    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    create() {
        if (this.creditForm.invalid) {
            return;
        }

        let creditPlan = new CreditPlanCreateDto();

        creditPlan.monthPeriod = this.creditForm.get('monthPeriod').value;
        creditPlan.name = this.creditForm.get('name').value;
        creditPlan.creditType = this.creditForm.get('creditType').value;
        creditPlan.percent = this.creditForm.get('percent').value;
        creditPlan.currencyId = this.creditForm.get('currencyId').value;

        this._creditPlanService.createPlan(creditPlan).subscribe(()=>{
            this._router.navigateByUrl('/credit-plan/list');
        })
    }


}
