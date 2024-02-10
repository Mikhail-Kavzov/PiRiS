import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { debounceTime,  Subject, switchMap, takeUntil } from 'rxjs';
import { DepositCreateDto, DepositPlanAgreementDto, ClientViewDto, SortDirection, ClientSortField } from '../../../../../api/api.client';
import { Patterns } from '../../../../core/enums/patterns.enum';
import { ClientService } from '../../client/client.service';
import { DepositService } from '../deposit.service';

@Component({
    selector: 'deposit-create',
    templateUrl: './deposit-create.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class DepositCreateComponent implements OnInit, OnDestroy {
    depositForm: FormGroup;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    plans: DepositPlanAgreementDto[];
    clients: ClientViewDto[];
    selectedCurrency: string = '';
    searchControl: UntypedFormControl = new UntypedFormControl();

    constructor(
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _depositService: DepositService,
        private _clientService: ClientService,
    ) {
    }

    ngOnInit(): void {

        this.depositForm = this._formBuilder.group({
            depositPlanId: ['', Validators.required],
            clientId: ['', Validators.required],
            sum: [1, [Validators.required, Validators.min(1)]],
            depositNumber: ['',[Validators.required, Validators.pattern(Patterns.DepositNumber)]]
        })

        this._depositService.plans$.pipe(takeUntil(this._unsubscribeAll))
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

    planHandler(plan: DepositPlanAgreementDto) {
        this.selectedCurrency = plan.currencyName;
    }

    create() {
        if (this.depositForm.invalid) {
            return;
        }

        let deposit = new DepositCreateDto();

        deposit.clientId = this.depositForm.get('clientId').value;
        deposit.depositNumber = this.depositForm.get('depositNumber').value;
        deposit.depositPlanId = this.depositForm.get('depositPlanId').value;
        deposit.sum = this.depositForm.get('sum').value;

        this._depositService.createDeposit(deposit).subscribe(()=>{
            this._router.navigateByUrl('');
        })
    }

    trackClients(index: number, item: ClientViewDto): any {
        return item.clientId || index;
    }

}
