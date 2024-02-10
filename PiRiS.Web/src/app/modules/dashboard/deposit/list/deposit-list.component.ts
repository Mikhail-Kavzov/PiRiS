import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { debounceTime, Subject, switchMap, takeUntil } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { Pagination } from '../../../../../types/pagination.types';
import { DepositService } from '../deposit.service';
import { Router } from '@angular/router';
import { DepositDto } from '../../../../../api/api.client';

@Component({
    selector: 'deposit-list',
    templateUrl: './deposit-list.component.html',
    styleUrls: ['./deposit-list.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class DepositListComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    deposits: DepositDto[];
    pagination: Pagination;

    searchInputControl: UntypedFormControl = new UntypedFormControl();

    selectedDeposit: DepositDto | null = null;

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _depositService: DepositService,
        private _router: Router,

    ) {
    }

    ngOnInit(): void {


        this._depositService.pagination$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagination: Pagination) => {

                this.pagination = pagination;

                this._changeDetectorRef.markForCheck();
            });

        this._depositService.deposits$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((deposits: DepositDto[]) => {

                this.deposits = deposits;

                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(300),
                switchMap((query) => {
                    this.closeDetails();
                    query = query ?? '';

                    return this._depositService.getDeposits(0, this._paginator.pageSize, query);
                })
            )
            .subscribe();

    }

    ngAfterViewInit(): void {
        if (this._paginator) {

            this._changeDetectorRef.markForCheck();

            this._paginator.page.pipe(
                switchMap(() => {
                    this.closeDetails();
                    let search = this.searchInputControl.value ?? '';

                    return this._depositService.getDeposits(this._paginator.pageIndex, this._paginator.pageSize, search);
                })
            ).subscribe();
        }
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    toggleDetails(deposit: DepositDto): void {

        if (this.selectedDeposit && this.selectedDeposit.depositId === deposit.depositId) {

            this.closeDetails();
            return;
        }
        this.selectedDeposit = deposit;
        this._changeDetectorRef.markForCheck();
    }

    closeDetails(): void {
        this.selectedDeposit = null;
    }

    trackByFn(index: number, item: DepositDto): any {
        return item.depositId || index;
    }

    closeDeposit() {
        this._depositService.closeDeposit(this.selectedDeposit.depositId).subscribe(() => {
            this.selectedDeposit.canClose = false;
            this.selectedDeposit.canWithdraw = false;
        })
    }

    withdrawPercents() {
        this._depositService.withdrawPercents(this.selectedDeposit.depositId).subscribe(() => {
            this.selectedDeposit.canWithdraw = false;
        })
    }
}
