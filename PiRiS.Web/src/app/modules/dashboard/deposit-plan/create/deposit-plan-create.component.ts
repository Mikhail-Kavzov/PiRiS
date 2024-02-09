import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Subject } from 'rxjs';
import { CreditPlanCreateDto, CurrencyDto, DepositPlanCreateDto } from '../../../../../api/api.client';
import { DepositPlanService } from '../deposit-plan.service';

@Component({
    selector: 'deposit-plan-create',
    templateUrl: './deposit-plan-create.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class DepositPlanCreateComponent implements OnInit, OnDestroy {
    depositForm: FormGroup;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    currencies: CurrencyDto[];

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _depositPlanService:DepositPlanService
    ) {
    }

    ngOnInit(): void {

        this.depositForm = this._formBuilder.group({
            dayPeriod: [1, [Validators.required, Validators.min(1)]],
            name: ['', Validators.required],
            depositType: [0, Validators.required],
            percent: [1, [Validators.required, Validators.min(0)]],
            currencyId: [1, Validators.required],
        })


    }
    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    create() {
        if (this.depositForm.invalid) {
            return;
        }

        let depositPlan = new DepositPlanCreateDto();

        depositPlan.dayPeriod = this.depositForm.get('dayPeriod').value;
        depositPlan.name = this.depositForm.get('name').value;
        depositPlan.depositType = this.depositForm.get('depositType').value;
        depositPlan.percent = this.depositForm.get('percent').value;
        depositPlan.currencyId = this.depositForm.get('currencyId').value;

        this._depositPlanService.createPlan(depositPlan).subscribe(()=>{
            this._router.navigateByUrl('');
        })
    }


}
