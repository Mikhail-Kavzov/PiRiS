import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { debounceTime, Subject, switchMap, takeUntil } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { Pagination } from '../../../../../types/pagination.types';
import { CreditService } from '../credit.service';
import { Router } from '@angular/router';
import { CreditDto } from '../../../../../api/api.client';

@Component({
    selector: 'credit-list',
    templateUrl: './credit-list.component.html',
    styleUrls: ['./credit-list.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class CreditListComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    credits: CreditDto[];
    pagination: Pagination;

    searchInputControl: UntypedFormControl = new UntypedFormControl();

    selectedCredit: CreditDto | null = null;

    messageText: string = '';
    displayMessage: boolean = false;

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _creditService: CreditService,
        private _router: Router,

    ) {
    }

    ngOnInit(): void {


        this._creditService.pagination$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagination: Pagination) => {

                this.pagination = pagination;

                this._changeDetectorRef.markForCheck();
            });

        this._creditService.credits$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((credits: CreditDto[]) => {

                this.credits = credits;

                this._changeDetectorRef.markForCheck();
            });

        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(300),
                switchMap((query) => {
                    this.closeDetails();
                    query = query ?? '';

                    return this._creditService.getCredits(0, this._paginator.pageSize, query);
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

                    return this._creditService.getCredits(this._paginator.pageIndex, this._paginator.pageSize, search);
                })
            ).subscribe();
        }
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    toggleDetails(credit: CreditDto): void {

        if (this.selectedCredit && this.selectedCredit.creditId === credit.creditId) {

            this.closeDetails();
            return;
        }
        this.selectedCredit = credit;
        this._changeDetectorRef.markForCheck();
    }

    closeDetails(): void {
        this.selectedCredit = null;
    }

    trackByFn(index: number, item: CreditDto): any {
        return item.creditId || index;
    }

    closeCredit() {
        this.selectedCredit.canClose = false;
        this.selectedCredit.canPayPercents = false;
        this._creditService.closeCredit(this.selectedCredit.creditId)
            .subscribe(() => {
                this.showSuccess('Credit was closed');
            },
                () => {
                    this.showError();
                })
    }

    payPercents() {
        this.selectedCredit.canPayPercents = false;
        this._creditService.payPercents(this.selectedCredit.creditId)
            .subscribe(() => {

                this.showSuccess('Percents were successfully paid');
            },
                () => {
                    this.showError();
                });
    }

    showSchedule() {
        let creditId = this.selectedCredit.creditId;
        this._router.navigateByUrl(`/credit/schedule?creditId=${creditId}`);
    }

    hideMessage() {
        this._changeDetectorRef.markForCheck();
        setTimeout(() => {
            this.displayMessage = false;
            this._changeDetectorRef.markForCheck();
        }, 3000);
    }

    showSuccess(text: string) {
        this.messageText = text;
        this.displayMessage = true;
        this.hideMessage();
    }

    showError() {
        this.messageText = 'Error occured';
        this.displayMessage = true;
        this.hideMessage();
    }
}
