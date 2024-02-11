import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { debounceTime, Subject, switchMap, takeUntil } from 'rxjs';
import { CreditCreateDto, CreditPlanAgreementDto,  ClientViewDto, SortDirection, ClientSortField } from '../../../../../api/api.client';
import { Patterns } from '../../../../core/enums/patterns.enum';
import { ClientService } from '../../client/client.service';
import { CreditService } from '../credit.service';

@Component({
    selector: 'credit-create',
    templateUrl: './credit-create.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class CreditCreateComponent implements OnInit, OnDestroy {
    creditForm: FormGroup;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    plans: CreditPlanAgreementDto[];
    clients: ClientViewDto[];
    selectedCurrency: string = '';
    searchControl: UntypedFormControl = new UntypedFormControl();
    selectedPlan: CreditPlanAgreementDto;

    constructor(
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _creditService: CreditService,
        private _clientService: ClientService,
    ) {
    }

    ngOnInit(): void {

        this.creditForm = this._formBuilder.group({
            creditPlanId: ['', Validators.required],
            clientId: ['', Validators.required],
            sum: [1, [Validators.required, Validators.min(1)]],
            creditNumber: ['', [Validators.required, Validators.pattern(Patterns.CreditNumber)]],
            creditCardNumber: ['', [Validators.required, Validators.pattern(Patterns.CreditCardNumber)]],
            creditCardCode: ['',[Validators.required, Validators.pattern(Patterns.CreditCardCode)]]
        })

        this._creditService.plans$.pipe(takeUntil(this._unsubscribeAll))
            .subscribe((plans) => {
                this.plans = plans;
            })

        this._clientService.clients$.pipe(takeUntil(this._unsubscribeAll))
            .subscribe((clients) => {
                this.clients = clients;
            })

        this.searchControl.valueChanges.pipe(
            takeUntil(this._unsubscribeAll),
            debounceTime(300),
            switchMap((query) => {
                query = query ?? '';

                return this._clientService.getClients(0, 10, query, SortDirection.ascending, ClientSortField.surname);
            })).subscribe();

    }
    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    planHandler(plan: CreditPlanAgreementDto) {
        this.selectedCurrency = plan.currencyName;
        this.selectedPlan = plan;
    }

    create() {
        if (this.creditForm.invalid) {
            return;
        }

        let credit = new CreditCreateDto();

        credit.clientId = this.creditForm.get('clientId').value;
        credit.creditNumber = this.creditForm.get('creditNumber').value;
        credit.creditPlanId = this.creditForm.get('creditPlanId').value;
        credit.sum = this.creditForm.get('sum').value;
        this.creditForm.get('creditCardNumber').value;
        this.creditForm.get('creditCardCode').value;

        this._creditService.createCredit(credit).subscribe(()=>{
            this._router.navigateByUrl('/credit/list');
        })
    }

    trackClients(index: number, item: ClientViewDto): any {
        return item.clientId || index;
    }

}
