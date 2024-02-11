import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { debounceTime, Subject, switchMap, takeUntil } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { Pagination } from '../../../../../types/pagination.types';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { AccountDto } from '../../../../../api/api.client';

@Component({
    selector: 'account-list',
    templateUrl: './account-list.component.html',
    styleUrls: ['./account-list.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class AccountListComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    accounts: AccountDto[];
    pagination: Pagination;

    searchInputControl: UntypedFormControl = new UntypedFormControl();

    selectedAccount: AccountDto | null = null;

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _accountService: AccountService,
        private _router: Router,

    ) {
    }

    ngOnInit(): void {


        this._accountService.pagination$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagination: Pagination) => {

                this.pagination = pagination;

                this._changeDetectorRef.markForCheck();
            });

        this._accountService.accounts$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((accounts: AccountDto[]) => {

                this.accounts = accounts;

                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(300),
                switchMap((query) => {
                    this.closeDetails();
                    query = query ?? '';

                    return this._accountService.getAccounts(0, this._paginator.pageSize, query);
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

                    return this._accountService.getAccounts(this._paginator.pageIndex, this._paginator.pageSize, search);
                })
            ).subscribe();
        }
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    toggleDetails(account: AccountDto): void {

        if (this.selectedAccount && this.selectedAccount.accountId === account.accountId) {

            this.closeDetails();
            return;
        }
        this.selectedAccount = account;
        this._changeDetectorRef.markForCheck();
    }

    closeDetails(): void {
        this.selectedAccount = null;
    }

    trackByFn(index: number, item: AccountDto): any {
        return item.accountId || index;
    }
}
