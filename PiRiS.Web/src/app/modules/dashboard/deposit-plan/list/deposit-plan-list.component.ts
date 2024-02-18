import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { DepositPlanDto, } from '../../../../../api/api.client';
import { Pagination } from '../../../../../types/pagination.types';
import { DepositPlanService } from '../deposit-plan.service';

@Component({
    selector: 'deposit-plan-list',
    templateUrl: './deposit-plan-list.component.html',
    styleUrls: ['./deposit-plan-list.component.scss'],
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: fuseAnimations
})
export class DepositPlanListComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild(MatPaginator) private _paginator: MatPaginator;

    plans: DepositPlanDto[];
    pagination: Pagination;


    private _unsubscribeAll: Subject<any> = new Subject<any>();
    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _depositPlanService: DepositPlanService)
    {
    }

    ngOnInit(): void {

        this._depositPlanService.pagination$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((pagination: Pagination) => {

                this.pagination = pagination;

                this._changeDetectorRef.markForCheck();
            });

        this._depositPlanService.plans$.pipe(takeUntil(this._unsubscribeAll))
            .subscribe((plans: DepositPlanDto[]) => {
            this.plans = plans;
            this._changeDetectorRef.markForCheck();
        })
    }

    ngAfterViewInit(): void {
        if (this._paginator) {

            this._changeDetectorRef.markForCheck();

            this._paginator.page.pipe(
                switchMap(() => {
                    return this._depositPlanService.getPlans(this._paginator.pageIndex, this._paginator.pageSize);
                })
            ).subscribe();
        }
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }


    trackByFn(index: number, item: DepositPlanDto): any {
        return item.depositPlanId || index;
    }
}
